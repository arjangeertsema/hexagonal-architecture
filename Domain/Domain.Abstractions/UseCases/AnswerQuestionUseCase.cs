namespace Domain.Abstractions.UseCases;

public class AnswerQuestionUseCase : Command, IUserTaskId
{
    public AnswerQuestionUseCase(Guid commandId, AnswerQuestionId questionId, string userTaskId, string answer) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        QuestionId = questionId;
        UserTaskId = userTaskId;
        Answer = answer;
    }

    public AnswerQuestionId QuestionId { get; }
    public string UserTaskId { get; set; }
    public string Answer { get; set; }
}
