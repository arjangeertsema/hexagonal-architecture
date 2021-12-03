namespace Domain.Abstractions.Models;

public class Recipient : ValueObject
{
    public string Name { get; }
    public string EmailAddress { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return EmailAddress;
    }
}
