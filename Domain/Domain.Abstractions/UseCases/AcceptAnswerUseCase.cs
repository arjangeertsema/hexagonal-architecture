namespace Domain.Abstractions.UseCases;

public class AcceptAnswerUseCase : Command, IHasUserTask
{
    public AcceptAnswerUseCase(Guid commandId, AnswerQuestionId questionId, IUserTask userTask) : base(commandId)
    {
        QuestionId = questionId;
        UserTask = userTask;
    }

    public AnswerQuestionId QuestionId { get; }
    public IUserTask UserTask { get; }
}
