using System;
using System.Threading.Tasks;

namespace Reference.Domain.Abstractions.DDD
{
    public interface IAggregateRootStore
    {
        Task<TAggregateRoot> Get<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;
        Task Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}