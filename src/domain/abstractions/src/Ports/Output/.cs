using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public interface IHandleDomainEventPort<TEvent> : IOutputPort<IHandleDomainEventPort<TEvent>.Command>
        where TEvent : IDomainEvent
    {
        public class Command : ICommand
        {
            public Command(Guid commandId, TEvent @event)
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
}