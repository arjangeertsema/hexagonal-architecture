using System;
using Reference.Domain.Abstractions.DDD;

namespace Example.Adapters.DDD
{
    [Serializable]
    public class DomainEventHandlerNotFoundException : Exception
    {
        public DomainEventHandlerNotFoundException(IDomainEvent @event)
            : base($"No service found for IHandleDomainEventPort<{@event.GetType().Name}>.")
        {
            Event = @event;
        }
        public IDomainEvent Event { get; }
    }
}