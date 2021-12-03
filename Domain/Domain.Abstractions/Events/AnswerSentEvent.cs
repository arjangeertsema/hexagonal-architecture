namespace Domain.Abstractions.Events;

public class AnswerSentEvent : VersionedDomainEvent<QuestionId>
{

    public AnswerSentEvent(QuestionId aggregateId, DateTime sent)
        : base(aggregateId)

    {
        Sent = sent;
    }

    public DateTime Sent { get; }
}
