namespace Domain.Abstractions.UseCases;

public class AcceptAnswerUseCase : ICommand, IUserTaskId
{
    public AcceptAnswerUseCase(Guid commandId, AnswerQuestionId questionId, string userTaskId)
    {
        CommandId = commandId;
        QuestionId = questionId;
        UserTaskId = userTaskId;
    }

    public Guid CommandId { get; }
    public AnswerQuestionId QuestionId { get; }
    public string UserTaskId { get; }
}
