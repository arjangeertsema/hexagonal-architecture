namespace Domain.Abstractions.UseCases;

public class AnswerQuestionUseCase : Command, IHasUserTaskId
{
    public AnswerQuestionUseCase(Guid commandId, AnswerQuestionId questionId, IUserTaskId userTaskId, string answer) : base(commandId)
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
    public IUserTaskId UserTaskId { get; }
    public string Answer { get; }
}
