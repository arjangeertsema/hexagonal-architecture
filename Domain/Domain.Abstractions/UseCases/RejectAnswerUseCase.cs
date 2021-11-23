namespace Domain.Abstractions.UseCases;

public class RejectAnswerUseCase : Command, IUserTaskId
{
    public RejectAnswerUseCase(Guid commandId, AnswerQuestionId questionId, string userTaskId, string rejection) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        QuestionId = questionId;
        UserTaskId = userTaskId;
    }

    public AnswerQuestionId QuestionId { get; }
    public string UserTaskId { get; set; }
    public string Rejection { get; set; }
}
