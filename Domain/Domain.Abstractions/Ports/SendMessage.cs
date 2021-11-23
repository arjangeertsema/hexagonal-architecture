namespace Domain.Abstractions.Ports;

public class SendMessage : Command
{
    public SendMessage(Message message) : base(Guid.NewGuid())
    {
        Message = message;
    }

    public Message Message { get; }
}
