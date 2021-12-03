namespace Domain.Abstractions.UseCases;

public class ModifyAnswerUseCase : Command, IHasUserTaskId
{
    public ModifyAnswerUseCase(Guid commandId, QuestionId questionId, IUserTaskId userTaskId, string answer) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
        UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
        Answer = answer;
    }

    public QuestionId QuestionId { get; }
    public IUserTaskId UserTaskId { get; }
    public string Answer { get; }
}
