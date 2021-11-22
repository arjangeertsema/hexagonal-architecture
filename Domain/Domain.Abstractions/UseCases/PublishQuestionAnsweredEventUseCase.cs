namespace Domain.Abstractions.UseCases;

public class PublishQuestionAnsweredEventUseCase : ICommand
{
    public PublishQuestionAnsweredEventUseCase(Guid commandId, AnswerQuestionId questionId)
    {
        CommandId = commandId;
        QuestionId = questionId;
    }

    public Guid CommandId { get; }
    public AnswerQuestionId QuestionId { get; }
}
