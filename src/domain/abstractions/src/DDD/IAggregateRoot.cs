using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.DDD
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        IEnumerable<IDomainEvent> GetChanges();
        void ClearChanges();
    }
}