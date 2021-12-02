namespace Domain.Abstractions.UseCases;

public class EndQuestionUseCase : Command
{
    public EndQuestionUseCase(Guid commandId, AnswerQuestionId questionId) : base(commandId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }

    public AnswerQuestionId QuestionId { get; }
}
