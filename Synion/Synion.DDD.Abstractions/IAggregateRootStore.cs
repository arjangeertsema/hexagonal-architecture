using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synion.DDD.Abstractions
{
    public interface IAggregateRootStore<TAggregateRoot>
         where TAggregateRoot : AggregateRoot
    {
        Task<TAggregateRoot> Get(Guid aggregateRootId, CancellationToken cancellationToken);
        Task Save(Guid commandId, TAggregateRoot aggregateRoot, CancellationToken cancellationToken);
    }
}