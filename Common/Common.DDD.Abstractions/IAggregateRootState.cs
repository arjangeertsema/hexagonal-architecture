using System;

namespace Common.DDD.Abstractions
{
    public interface IAggregateRootState
    {
        Guid Id { get; }
    }
}