namespace Domain.Abstractions.Ports;

public class PublishEvent<TEvent> : Command
    where TEvent : IEvent
{
    public PublishEvent(TEvent @event) : base(Guid.NewGuid())
    {
        Event = @event;
    }

    public TEvent Event { get; }
}
