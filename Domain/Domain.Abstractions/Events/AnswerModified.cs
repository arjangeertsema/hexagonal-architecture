namespace Domain.Abstractions.Events;

public class AnswerModifiedEvent : VersionedDomainEvent<QuestionId>
{
    public AnswerModifiedEvent(QuestionId aggregateId, string answer, string modifiedBy, DateTime modified)
        : base(aggregateId)

    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        if (string.IsNullOrWhiteSpace(modifiedBy))
        {
            throw new ArgumentException($"'{nameof(modifiedBy)}' cannot be null or whitespace.", nameof(modifiedBy));
        }

        Answer = answer;
        ModifiedBy = modifiedBy;
        Modified = modified;
    }

    public string Answer { get; }
    public string ModifiedBy { get; }
    public DateTime Modified { get; }
}
