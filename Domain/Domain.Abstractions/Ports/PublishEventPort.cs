using System;
using Common.CQRS.Abstractions;

namespace Domain.Abstractions.Ports
{
    public class PublishEventPort<TEvent> : ICommand
        where TEvent : IEvent
    {
        public PublishEventPort(Guid commandId, TEvent @event)
        {
            CommandId = commandId;
            Event = @event;
        }

        public Guid CommandId { get; }
        public TEvent Event { get; }
    }
}