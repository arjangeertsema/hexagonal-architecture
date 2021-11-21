namespace Domain.Abstractions.Events;

public class AnswerAcceptedEvent : VersionedDomainEvent<AnswerQuestionId>, IUserTaskId
{
    public AnswerAcceptedEvent(AnswerQuestionId aggregateId, string userTaskId, string acceptedBy, DateTime accepted)
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

    public string UserTaskId { get; }
    public string AcceptedBy { get; }
    public DateTime Accepted { get; }
}
