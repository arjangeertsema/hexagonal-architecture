using System;

namespace Synion.CQRS.Abstractions.Events
{
    public interface IEvent
    {
         Guid EventId { get; }
    }
}