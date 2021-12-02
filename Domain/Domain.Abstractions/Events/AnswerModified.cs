namespace Domain.Abstractions.Events;

public class AnswerModifiedEvent : VersionedDomainEvent<AnswerQuestionId>, IHasUserTaskId
{
    public AnswerModifiedEvent(AnswerQuestionId aggregateId, IUserTaskId userTaskId, string answer, string modifiedBy, DateTime modified)
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

        UserTaskId = userTaskId;
        Answer = answer;
        ModifiedBy = modifiedBy;
        Modified = modified;
    }

    public IUserTaskId UserTaskId { get; }
    public string Answer { get; }
    public string ModifiedBy { get; }
    public DateTime Modified { get; }
}
