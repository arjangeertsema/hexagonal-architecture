namespace Domain.Abstractions.Ports;

public class SendMessage : ICommand
{
    public SendMessage(Guid commandId, Message message)
    {
        CommandId = commandId;
        Message = message;

    }

    public Guid CommandId { get; }
    public Message Message { get; }
}
