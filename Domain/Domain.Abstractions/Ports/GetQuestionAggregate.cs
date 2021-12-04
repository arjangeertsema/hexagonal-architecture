namespace Domain.Abstractions.Ports;

public class GetQuestionAggregate : IQuery<IQuestion>
{
    public QuestionId QuestionId { get; }

    public GetQuestionAggregate(QuestionId questionId)
    {
        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }
}
