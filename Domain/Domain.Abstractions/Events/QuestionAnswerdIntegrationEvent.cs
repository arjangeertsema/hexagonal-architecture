namespace Domain.Abstractions.Events;

public class QuestionAnswerdIntegrationEvent : IEvent
{
    public QuestionAnswerdIntegrationEvent(QuestionId questionId)
    {
        this.EventId = Guid.NewGuid();
        this.QuestionId = questionId;
    }

    public Guid EventId { get; }
    public QuestionId QuestionId { get; }
}
