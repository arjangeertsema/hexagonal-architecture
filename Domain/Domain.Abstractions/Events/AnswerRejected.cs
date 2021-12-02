namespace Domain.Abstractions.Events;

public class AnswerRejectedEvent : VersionedDomainEvent<AnswerQuestionId>, IHasUserTaskId
{
    public AnswerRejectedEvent(AnswerQuestionId aggregateId, IUserTaskId userTaskId, string rejection, string rejectedBy, DateTime rejected)
        : base(aggregateId)

    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        if (string.IsNullOrWhiteSpace(rejectedBy))
        {
            throw new ArgumentException($"'{nameof(rejectedBy)}' cannot be null or whitespace.", nameof(rejectedBy));
        }

        UserTaskId = userTaskId;
        Rejection = rejection;
        RejectedBy = rejectedBy;
        Rejected = rejected;
    }

    public IUserTaskId UserTaskId { get; }
    public string Rejection { get; }
    public string RejectedBy { get; }
    public DateTime Rejected { get; }
}
