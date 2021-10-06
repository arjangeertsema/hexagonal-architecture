using System;
using Synion.DDD.Abstractions;
using Synion.CQRS.Abstractions.Ports;

namespace Domain.Abstractions.Ports.Output
{
    public class PublishDomainEventPort<TEvent> : IOutputPort
            where TEvent : IDomainEvent
    {
        public PublishDomainEventPort(Guid commandId, TEvent @event)
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