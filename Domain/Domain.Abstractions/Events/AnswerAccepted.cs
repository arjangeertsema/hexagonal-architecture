namespace Domain.Abstractions.Events;

public class AnswerAcceptedEvent : VersionedDomainEvent<AnswerQuestionId>, IHasUserTask
{
    public AnswerAcceptedEvent(AnswerQuestionId aggregateId, IUserTask userTask, string acceptedBy, DateTime accepted)
        : base(aggregateId)
    {
        if (string.IsNullOrWhiteSpace(acceptedBy))
        {
            throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
        }

        UserTask = userTask;
        AcceptedBy = acceptedBy;
        Accepted = accepted;
    }

    public IUserTask UserTask { get; }
    public string AcceptedBy { get; }
    public DateTime Accepted { get; }
}
