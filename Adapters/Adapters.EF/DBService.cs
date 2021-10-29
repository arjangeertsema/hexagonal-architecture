using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adapters.EF.Models;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Attributes;
using Common.CQRS.Abstractions.Queries;
using Common.DDD.Abstractions;
using Common.DDD.Abstractions.Commands;
using Common.DDD.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.EF
{
    [ServiceLifetime(ServiceLifetime.Scoped)]
    public class DBService<TEvent> :
        ICommandHandler<SaveAggregateRootEvent<TEvent>>,
        IQueryHandler<GetAggregateRootEvents, IEnumerable<IVersionedDomainEvent>>
        where TEvent : IVersionedDomainEvent
    {
        private readonly DBContext dbContext;
        private readonly ISerializer serializer;
        private readonly IDeserializer deserializer;
        public DBService(DBContext dbContext, ISerializer serializer, IDeserializer deserializer)
        {
            this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
            this.serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            this.deserializer = deserializer ?? throw new System.ArgumentNullException(nameof(deserializer));
        }

        public async Task Handle(SaveAggregateRootEvent<TEvent> command, CancellationToken cancellationToken)
        {
            var @event = (IVersionedDomainEvent)command.Event;
            
            await CheckVersions(@event);

            var serialized = await serializer.Serialize(@event);

            var model = new DomainEventModel()
            {
                EventId = @event.EventId,
                AggregateRootId = @event.AggregateRootId,
                Version = @event.Version,
                Type = @event.GetType().FullName,
                Event = serialized
            };

            await dbContext.DomainEvents.AddAsync(model);
        }

        private async Task CheckVersions(IVersionedDomainEvent @event)
        {
            var version = await dbContext.DomainEvents
                .Where(e => e.AggregateRootId == @event.AggregateRootId)
                .Select(e => e.Version)
                .DefaultIfEmpty()
                .MaxAsync();

            if ((@event.Version == 1 && version != default(long)) ||
                (version == default(long) && @event.Version != 1) ||
                (version != default(long) && version == @event.Version - 1)) 
            {                
                throw new Exception($"Database version {version} and event version {@event.Version} are out of bounds.");
            }
        }

        public async Task<IEnumerable<IVersionedDomainEvent>> Handle(GetAggregateRootEvents query, CancellationToken cancellationToken)
        {
            var events = await dbContext.DomainEvents
                .Where(e => e.AggregateRootId == query.AggregateRootId)
                .OrderBy(e => e.Version)
                .ToListAsync();

            var tasks = events
                .Select(e => {
                    var type = Type.GetType(e.Type);
                    return deserializer.Deserialize(type, e.Event);
                });

            await Task.WhenAll(tasks);

            return tasks.Select(t => (IVersionedDomainEvent)t.Result);
        }
    }
}