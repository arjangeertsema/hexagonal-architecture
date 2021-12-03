namespace Domain.Abstractions;

public class QuestionId : EntityId
{
    public Guid Id { get; private set; }

    public QuestionId(Guid id) => this.Id = id;

    public QuestionId(string id) : base(id) { }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Id;
    }

    protected override void ParseIdParts(IEnumerable<string> parts)
    {
        if(parts.Count() != 1)
            throw new ArgumentOutOfRangeException(nameof(parts), $"Expected 1 part but recieved {parts.Count()} parts.");

        this.Id = Guid.Parse(parts.First());
    }
}
