using System;
using System.Threading.Tasks;

namespace Reference.Domain.Abstractions.DDD
{
    public interface IAggregateRootStore
    {
        Task<TAggregateRoot> Get<TAggregateRoot>(Guid aggregateRootId) where TAggregateRoot : AggregateRoot;
        Task Save<TAggregateRoot>(Guid commandId, TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}