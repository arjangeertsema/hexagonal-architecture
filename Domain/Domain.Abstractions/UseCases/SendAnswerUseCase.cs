namespace Domain.Abstractions.UseCases;

public class SendAnswerUseCase : ICommand
{
    public SendAnswerUseCase(Guid commandId, AnswerQuestionId questionId)
    {
        CommandId = commandId;
        QuestionId = questionId;
    }

    public Guid CommandId { get; }
    public AnswerQuestionId QuestionId { get; }
}
