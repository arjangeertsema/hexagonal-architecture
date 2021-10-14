/*using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Ports.Output;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Commands;
using Common.DDD.Abstractions;
using Common.DDD.Abstractions.Exceptions;

namespace Adapters.Storage.Configuration
{
    public class AggregateRootStore<TAggregateRoot> : IAggregateRootStore<TAggregateRoot>
         where TAggregateRoot : IAggregateRoot
    {
        private readonly IMediator mediator;

        public AggregateRootStore(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TAggregateRoot> Get(Guid aggregateRootId, CancellationToken cancellationToken)
        {
            var state = await mediator.Ask(new GetAggregateRootStatePort(aggregateRootId), cancellationToken);
            if (state == null)
                throw new NotFoundException($"AggregateRoot state for type `{typeof(TAggregateRoot).Name}` with id `${aggregateRootId}` is not found.");

            var aggregateRoot = Activator.CreateInstance(typeof(TAggregateRoot), new object[] { aggregateRootId, state }) as TAggregateRoot;
            return aggregateRoot;
        }

        public Task Save(Guid commandId, TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        {
            var changes = aggregateRoot.Commit();
            return Send(commandId, changes, cancellationToken)
                .ContinueWith((r) => Notify(changes, cancellationToken));
        }

        private async Task Send(Guid commandId, IEnumerable<IDomainEvent> changes, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            
            foreach (var @event in changes)
            {
                var task = mediator.Send(CreateSaveDomainEventPort(commandId, @event), cancellationToken)
                    .ContinueWith((s) =>
                        mediator.Notify(@event, cancellationToken)
                    );

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private async Task Notify( IEnumerable<IDomainEvent> changes, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            
            foreach (var @event in changes)
            {
                var task = mediator.Notify(@event, cancellationToken);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private ICommand CreateSaveDomainEventPort(Guid commandId, IDomainEvent @event)
        {
            var type = typeof(SaveDomainEventPort<>);
            var genericType = type.MakeGenericType(@event.GetType());
            var command = Activator.CreateInstance(genericType, new object[] { commandId, @event });
            return command as ICommand;
        }
    }
}
*/