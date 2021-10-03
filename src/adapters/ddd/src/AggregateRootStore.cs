using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.DDD.Exceptions;
using Reference.Domain.Abstractions.Ports.Output;

namespace Example.Adapters.DDD
{
    public class AggregateRootStore : IAggregateRootStore
    {
        private readonly IMediator mediator;

        public AggregateRootStore(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }
        
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