namespace Domain.Abstractions.UseCases;

public class RejectAnswerUseCase : ICommand, IUserTaskId
{
    public RejectAnswerUseCase(Guid commandId, AnswerQuestionId questionId, string userTaskId, string rejection)
    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        CommandId = commandId;
        QuestionId = questionId;
        UserTaskId = userTaskId;
    }

    public Guid CommandId { get; }
    public AnswerQuestionId QuestionId { get; }
    public string UserTaskId { get; set; }
    public string Rejection { get; set; }
}
