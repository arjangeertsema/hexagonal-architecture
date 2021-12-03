namespace Domain.Abstractions.Models;

public class Message : ValueObject
{
    public Recipient From { get; }
    public Recipient To { get; }
    public string Subject { get; }
    public string Body { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return From;
        yield return To;
        yield return Subject;
        yield return Body;
    }
}
