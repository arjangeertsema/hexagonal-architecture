using System;
using Common.CQRS.Abstractions.Events;

namespace Common.DDD.Abstractions
{
    public interface IDomainEvent : IEvent
    {
        Guid AggregateRootId { get;  }
    }
}