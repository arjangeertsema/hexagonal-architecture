namespace Domain.Abstractions.UseCases;

public class SendAnswerUseCase : Command
{
    public SendAnswerUseCase(Guid commandId, AnswerQuestionId questionId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }
    
    public AnswerQuestionId QuestionId { get; }
}
