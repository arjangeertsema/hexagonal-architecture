namespace Domain.Abstractions;

public class AnswerQuestionId : EntityId
{
    public Guid Id { get; private set; }

    public AnswerQuestionId(Guid id) => this.Id = id;

    public AnswerQuestionId(string id) : base(id) { }

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
