using System;
using Common.DDD.Abstractions;
using Common.CQRS.Abstractions.Commands;

namespace Domain.Abstractions.Ports.Output
{
    public class SaveDomainEventPort<TEvent> : ICommand
            where TEvent : IDomainEvent
    {
        public SaveDomainEventPort(Guid commandId, TEvent @event)
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