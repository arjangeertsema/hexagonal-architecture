using System;

namespace Common.DDD.Abstractions
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}