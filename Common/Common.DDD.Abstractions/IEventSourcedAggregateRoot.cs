using System.Collections.Generic;

namespace Common.DDD.Abstractions
{
    public interface IEventSourcedAggregateRoot : IAggregateRoot
    {
        long Version { get; }
        void ApplyEvent(IDomainEvent @event, long version);
        IEnumerable<IVersionedDomainEvent<IDomainEvent>> Commit();
    }
}