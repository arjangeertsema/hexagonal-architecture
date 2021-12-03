namespace Domain.Abstractions;

public interface IQuestion : IEventSourcedAggregateRoot<QuestionId>
{
    IDraftAnswer DraftAnswer { get; }
    void Answer(string answer, string answeredBy);
    QuestionAnswerdIntegrationEvent IsAnswered();
    void Revoke(string revokedBy);
}
