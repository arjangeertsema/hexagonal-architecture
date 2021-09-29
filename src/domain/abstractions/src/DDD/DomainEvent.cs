using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.DDD
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEvent(Guid aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        public Guid EventId { get; }
        public Guid AggregateId { get; }
    }
}