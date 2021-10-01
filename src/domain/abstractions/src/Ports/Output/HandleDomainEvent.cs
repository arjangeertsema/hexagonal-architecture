using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class HandleDomainEvent<TEvent> : IOutputPort
            where TEvent : IDomainEvent
    {
        public HandleDomainEvent(Guid commandId, TEvent @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            CommandId = commandId;
            Event = @event;
        }

        public Guid CommandId { get; }
        public TEvent Event { get; }
    }
}