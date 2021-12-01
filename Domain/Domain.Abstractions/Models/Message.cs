namespace Domain.Abstractions.Models;

public struct Message
{
    public Recipient From { get; }
    public Recipient To { get; }
    public string Subject { get; }
    public string Body { get; }
}
