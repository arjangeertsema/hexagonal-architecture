namespace Domain.Abstractions.UseCases;

public class EndQuestionUseCase : ICommand
{
    public EndQuestionUseCase(Guid commandId, AnswerQuestionId questionId)
    {
        CommandId = commandId;
        QuestionId = questionId;
    }

    public Guid CommandId { get; }
    public AnswerQuestionId QuestionId { get; set; }
}
