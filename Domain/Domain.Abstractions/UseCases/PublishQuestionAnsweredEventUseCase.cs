namespace Domain.Abstractions.UseCases;

public class PublishQuestionAnsweredEventUseCase : Command
{
    public PublishQuestionAnsweredEventUseCase(Guid commandId, QuestionId questionId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }
    
    public QuestionId QuestionId { get; }
}
