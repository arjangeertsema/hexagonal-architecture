using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;
using Synion.DDD.Abstractions.Exceptions;
using Domain.Abstractions.Ports.Output;

namespace Example.Adapters.Storage
{
    public class AggregateRootStore : IAggregateRootStore
    {
        private readonly IMediator mediator;

        public AggregateRootStore(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task<TAggregateRoot> Get<TAggregateRoot>(Guid aggregateRootId) where TAggregateRoot : AggregateRoot
        {
            var state = await mediator.Send(new GetAggregateRootStatePort(aggregateRootId));
            if (state == null)
                throw new NotFoundException($"AggregateRoot state for type `{typeof(TAggregateRoot).Name}` with id `${aggregateRootId}` is not found.");

            var aggregateRoot = Activator.CreateInstance(typeof(TAggregateRoot), new object[] { state }) as TAggregateRoot;
            return aggregateRoot;
        }

        public async Task Save<TAggregateRoot>(Guid commandId, TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            await mediator.Send(new SaveAggregateRootPort(commandId, aggregateRoot));
        }
    }
}