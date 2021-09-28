using System;

namespace example.domain.abstractions.ddd
{
    public interface IDomainEvent<TAggregateId>
    {
        Guid EventId { get; }
        TAggregateId AggregateId { get;  }
    }
}