namespace Domain.Abstractions.Events;

public class AnswerSentEvent : VersionedDomainEvent<AnswerQuestionId>
{

    public AnswerSentEvent(AnswerQuestionId aggregateId, DateTime sent)
        : base(aggregateId)

    {
        Sent = sent;
    }

    public DateTime Sent { get; }
}
