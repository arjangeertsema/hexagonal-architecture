namespace Domain.Abstractions.UseCases;

public class EndQuestionUseCase : Command
{
    public EndQuestionUseCase(Guid commandId, AnswerQuestionId questionId) : base(commandId)
    {
        QuestionId = questionId;
    }

    public AnswerQuestionId QuestionId { get; set; }
}
