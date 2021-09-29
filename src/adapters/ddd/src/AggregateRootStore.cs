using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.DDD.Exceptions;
using Reference.Domain.Abstractions.Ports.Output;

namespace Example.Adapters.DDD
{
    public class AggregateRootStore : IAggregateRootStore
    {
        private readonly IGetAggregateRootStatePort getAggregateRootStatePort;
        private readonly ISaveAggregateRootPort saveAggregateRootPort;

        public AggregateRootStore(IGetAggregateRootStatePort getAggregateRootStatePort,
            ISaveAggregateRootPort saveAggregateRootPort)
        {
            if (getAggregateRootStatePort is null)
            {
                throw new ArgumentNullException(nameof(getAggregateRootStatePort));
            }

            if (saveAggregateRootPort is null)
            {
                throw new ArgumentNullException(nameof(saveAggregateRootPort));
            }

            this.getAggregateRootStatePort = getAggregateRootStatePort;
            this.saveAggregateRootPort = saveAggregateRootPort;
        }
        public async Task<TAggregateRoot> Get<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            var state = await getAggregateRootStatePort.Execute(new IGetAggregateRootStatePort.Query(id));
            if (state == null)
                throw new NotFoundException($"AggregateRoot of type `{typeof(TAggregateRoot).Name}` with id `${id}` does not exist.");

            var aggregateRoot = Activator.CreateInstance(typeof(TAggregateRoot), new object[] { state }) as TAggregateRoot;
            return aggregateRoot;
        }

        public async Task Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            await saveAggregateRootPort.Execute(new ISaveAggregateRootPort.Command(Guid.NewGuid(), aggregateRoot));
        }
    }
}