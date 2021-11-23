namespace Domain.Abstractions.UseCases;

public class PublishQuestionAnsweredEventUseCase : Command
{
    public PublishQuestionAnsweredEventUseCase(Guid commandId, AnswerQuestionId questionId) : base(commandId)
    {
        QuestionId = questionId;
    }
    
    public AnswerQuestionId QuestionId { get; }
}
