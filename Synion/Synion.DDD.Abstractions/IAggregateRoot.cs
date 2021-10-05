using System;
using System.Collections.Generic;

namespace Synion.DDD.Abstractions
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        IEnumerable<IDomainEvent> Commit();
    }
}