using System;

namespace Common.CQRS.Abstractions.Events
{
    public interface IEvent
    {
         Guid EventId { get; }
    }
}