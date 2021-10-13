using System;

namespace Common.DDD.Abstractions
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEvent(Guid aggregateRootId) : this()
        {
            AggregateRootId = aggregateRootId;
        }

        public Guid EventId { get; }
        public Guid AggregateRootId { get; }
    }
}