namespace Domain.Abstractions.UseCases;

public class AcceptAnswerUseCase : Command, IHasUserTaskId
{
    public AcceptAnswerUseCase(Guid commandId, AnswerQuestionId questionId, IUserTaskId userTaskId) : base(commandId)
    {
        QuestionId = questionId;
        UserTaskId = userTaskId;
    }

    public AnswerQuestionId QuestionId { get; }
    public IUserTaskId UserTaskId { get; }
}
