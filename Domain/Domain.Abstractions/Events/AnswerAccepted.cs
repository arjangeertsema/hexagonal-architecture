namespace Domain.Abstractions.Events;

public class AnswerAcceptedEvent : VersionedDomainEvent<AnswerQuestionId>, IHasUserTaskId
{
    public AnswerAcceptedEvent(AnswerQuestionId aggregateId, IUserTaskId userTaskId, string acceptedBy, DateTime accepted)
        : base(aggregateId)
    {
        if (string.IsNullOrWhiteSpace(acceptedBy))
        {
            throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
        }

        UserTaskId = userTaskId;
        AcceptedBy = acceptedBy;
        Accepted = accepted;
    }

    public IUserTaskId UserTaskId { get; }
    public string AcceptedBy { get; }
    public DateTime Accepted { get; }
}
