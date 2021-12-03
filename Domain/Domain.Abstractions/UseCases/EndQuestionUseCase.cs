namespace Domain.Abstractions.UseCases;

public class EndQuestionUseCase : Command
{
    public EndQuestionUseCase(Guid commandId, QuestionId questionId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }

    public QuestionId QuestionId { get; }
}
