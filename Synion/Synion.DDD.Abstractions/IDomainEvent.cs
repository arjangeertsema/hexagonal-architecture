using System;
using Synion.CQRS.Abstractions.Events;

namespace Synion.DDD.Abstractions
{
    public interface IDomainEvent : IEvent
    {
        Guid AggregateRootId { get;  }
    }
}