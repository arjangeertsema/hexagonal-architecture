using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Ports.Output;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;
using Synion.DDD.Abstractions;
using Synion.DDD.Abstractions.Exceptions;

namespace Adapters.Storage.Configuration
{
    public class AggregateRootStore<TAggregateRoot> : IAggregateRootStore<TAggregateRoot>
         where TAggregateRoot : AggregateRoot
    {
        private readonly IMediator mediator;

        public AggregateRootStore(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TAggregateRoot> Get(Guid aggregateRootId, CancellationToken cancellationToken)
        {
            var state = await mediator.Send(new GetAggregateRootStatePort(aggregateRootId), cancellationToken);
            if (state == null)
                throw new NotFoundException($"AggregateRoot state for type `{typeof(TAggregateRoot).Name}` with id `${aggregateRootId}` is not found.");

            var aggregateRoot = Activator.CreateInstance(typeof(TAggregateRoot), new object[] { aggregateRootId, state }) as TAggregateRoot;
            return aggregateRoot;
        }

        public Task Save(Guid commandId, TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            var changes = aggregateRoot.Commit();

            foreach (var @event in changes)
            {
                var task = mediator.Send(CreateHandleDomainEventPort(commandId, @event), cancellationToken)
                    .ContinueWith((s) =>
                        mediator.Send(CreatePublishDomainEventPort(commandId, @event), cancellationToken)
                    );

                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }

        private ICommand CreateHandleDomainEventPort(Guid commandId, IDomainEvent @event)
        {
            var type = typeof(PublishDomainEventPort<>);
            var genericType = type.MakeGenericType(@event.GetType());
            var command = Activator.CreateInstance(genericType, new object[] { commandId, @event });
            return command as ICommand;
        }

        private ICommand CreatePublishDomainEventPort(Guid commandId, IDomainEvent @event)
        {
            var type = typeof(SaveDomainEventPort<>);
            var genericType = type.MakeGenericType(@event.GetType());
            var command = Activator.CreateInstance(genericType, new object[] { commandId, @event });
            return command as ICommand;
        }
    }
}