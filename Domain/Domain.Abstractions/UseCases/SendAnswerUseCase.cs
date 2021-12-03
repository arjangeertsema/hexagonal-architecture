namespace Domain.Abstractions.UseCases;

public class SendAnswerUseCase : Command
{
    public SendAnswerUseCase(Guid commandId, QuestionId questionId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }
    
    public QuestionId QuestionId { get; }
}
