using System;
using System.Collections.Generic;

namespace Common.DDD.Abstractions
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        IEnumerable<IDomainEvent> Commit();
    }
}