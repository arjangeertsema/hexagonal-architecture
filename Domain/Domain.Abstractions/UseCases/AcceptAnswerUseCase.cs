namespace Domain.Abstractions.UseCases;

public class AcceptAnswerUseCase : Command, IUserTaskId
{
    public AcceptAnswerUseCase(Guid commandId, AnswerQuestionId questionId, string userTaskId) : base(commandId)
    {
        QuestionId = questionId;
        UserTaskId = userTaskId;
    }

    public AnswerQuestionId QuestionId { get; }
    public string UserTaskId { get; }
}
