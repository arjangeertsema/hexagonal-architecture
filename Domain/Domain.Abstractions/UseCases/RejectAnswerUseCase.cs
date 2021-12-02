namespace Domain.Abstractions.UseCases;

public class RejectAnswerUseCase : Command, IHasUserTaskId
{
    public RejectAnswerUseCase(Guid commandId, AnswerQuestionId questionId, IUserTaskId userTaskId, string rejection) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        QuestionId = questionId;
        UserTaskId = userTaskId;
    }

    public AnswerQuestionId QuestionId { get; }
    public IUserTaskId UserTaskId { get; }
    public string Rejection { get; }
}
