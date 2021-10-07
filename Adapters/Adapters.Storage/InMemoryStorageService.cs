using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Ports.Output;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Ports;
using Synion.CQRS;
using System;
using Synion.DDD.Abstractions;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Storage
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class InMemoryStorageService<TEvent> :        
        IOutputPortHandler<SaveDomainEventPort<TEvent>>,
        IOutputPortHandler<GetAggregateRootStatePort, IEnumerable<KeyValuePair<string, string>>>
        where TEvent : IDomainEvent
    {
        private readonly Dictionary<Guid, Dictionary<string, string>> store;
        private readonly IMediator mediator;

        public InMemoryStorageService(IMediator mediator) 
        {
            this.store = new Dictionary<Guid, Dictionary<string, string>>();
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task Handle(SaveDomainEventPort<TEvent> command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (!this.store.ContainsKey(command.Event.AggregateRootId))
            {
                this.store.Add(command.Event.AggregateRootId, new Dictionary<string, string>());
            }

            Save(command.Event);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<KeyValuePair<string, string>>> Handle(GetAggregateRootStatePort query, CancellationToken cancellationToken)
        {
            if(!this.store.ContainsKey(query.AggregateRootId))
                return Task.FromResult((IEnumerable<KeyValuePair<string, string>>) null);

            return Task.FromResult((IEnumerable<KeyValuePair<string, string>>) this.store[query.AggregateRootId]);
        }

        private void Save(TEvent @event)
        {
            var state = this.store[@event.AggregateRootId];            
            var type = @event.GetType();
            var properties = type.GetProperties()
                .Where(p => p.CanRead && p.GetValue(@event) != null);

            foreach(var property in properties)
            {                
                state[property.Name] = Convert(property.GetValue(@event));
            }
        }

        private static string Convert(object value)
        {
            var converter = TypeDescriptor.GetConverter(value.GetType());
            return converter.ConvertToInvariantString(value);
        }
    }
}