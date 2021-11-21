namespace Domain.Abstractions.UseCases;

public class SendAnswerUseCase : ICommand
{
    public SendAnswerUseCase(Guid commandId, Guid questionId)
    {
        CommandId = commandId;
        QuestionId = questionId;
    }

    public Guid CommandId { get; }
    public Guid QuestionId { get; }
}
