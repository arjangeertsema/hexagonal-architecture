namespace Domain.Abstractions.UseCases;

public class RejectAnswerUseCase : Command, IHasUserTask
{
    public RejectAnswerUseCase(Guid commandId, AnswerQuestionId questionId, IUserTask userTask, string rejection) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        QuestionId = questionId;
        UserTask = userTask;
    }

    public AnswerQuestionId QuestionId { get; }
    public IUserTask UserTask { get; }
    public string Rejection { get; }
}
