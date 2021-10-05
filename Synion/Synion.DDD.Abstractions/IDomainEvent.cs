using System;

namespace Synion.DDD.Abstractions
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        Guid AggregateRootId { get;  }
    }
}