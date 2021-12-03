namespace Domain.Abstractions.Events;

public class AnswerAcceptedEvent : VersionedDomainEvent<QuestionId>
{
    public AnswerAcceptedEvent(QuestionId aggregateId, string acceptedBy, DateTime accepted)
        : base(aggregateId)
    {
        if (string.IsNullOrWhiteSpace(acceptedBy))
        {
            throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
        }

        AcceptedBy = acceptedBy;
        Accepted = accepted;
    }

    public string AcceptedBy { get; }
    public DateTime Accepted { get; }
}
