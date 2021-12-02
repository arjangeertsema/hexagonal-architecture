namespace Domain.Abstractions.UseCases;

public class PublishQuestionAnsweredEventUseCase : Command
{
    public PublishQuestionAnsweredEventUseCase(Guid commandId, AnswerQuestionId questionId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }
    
    public AnswerQuestionId QuestionId { get; }
}
