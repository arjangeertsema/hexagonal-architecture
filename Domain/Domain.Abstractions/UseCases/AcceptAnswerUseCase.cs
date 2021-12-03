namespace Domain.Abstractions.UseCases;

public class AcceptAnswerUseCase : Command, IHasUserTaskId
{
    public AcceptAnswerUseCase(Guid commandId, QuestionId questionId, IUserTaskId userTaskId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
        UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
    }

    public QuestionId QuestionId { get; }
    public IUserTaskId UserTaskId { get; }
}
