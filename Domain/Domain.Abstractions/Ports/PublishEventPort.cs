namespace Domain.Abstractions.Ports;

public class PublishEvent<TEvent> : ICommand
    where TEvent : IEvent
{
    public PublishEvent(Guid commandId, TEvent @event)
    {
        CommandId = commandId;
        Event = @event;
    }

    public Guid CommandId { get; }
    public TEvent Event { get; }
}
