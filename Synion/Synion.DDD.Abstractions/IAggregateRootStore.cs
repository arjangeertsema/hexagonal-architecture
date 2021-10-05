using System;
using System.Threading.Tasks;

namespace Synion.DDD.Abstractions
{
    public interface IAggregateRootStore
    {
        Task<TAggregateRoot> Get<TAggregateRoot>(Guid aggregateRootId) where TAggregateRoot : AggregateRoot;
        Task Save<TAggregateRoot>(Guid commandId, TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}