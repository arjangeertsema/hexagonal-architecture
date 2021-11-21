namespace Domain.Abstractions.UseCases;

public class PublishQuestionAnsweredEventUseCase : ICommand
{
    public PublishQuestionAnsweredEventUseCase(Guid commandId, Guid questionId)
    {
        CommandId = commandId;
        QuestionId = questionId;
    }

    public Guid CommandId { get; }
    public Guid QuestionId { get; }
}
