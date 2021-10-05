using System;

namespace Reference.Domain.Abstractions.DDD
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        Guid AggregateId { get;  }
    }
}