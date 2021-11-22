namespace Domain.Abstractions.Events;

public class QuestionAnswerdIntegrationEvent : IEvent
{
    public QuestionAnswerdIntegrationEvent(AnswerQuestionId questionId)
    {
        this.EventId = Guid.NewGuid();
        this.QuestionId = questionId;
    }

    public Guid EventId { get; }
    public AnswerQuestionId QuestionId { get; }
}
