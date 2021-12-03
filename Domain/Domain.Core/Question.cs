namespace Domain.Core;

public class Question : EventSourcedAggregateRoot<QuestionId>, IQuestion
{
    private string subject;
    private string question;
    private string askedBy;
    private DateTime asked;
    private DraftAnswer draftAnswer;

    public static Question Create(QuestionId questionId, string subject, string question, string askedBy)
    {
        return new Question
        (
            id: questionId,
            subject: subject,
            question: question,
            askedBy: askedBy
        );
    }

    public IDraftAnswer DraftAnswer { get { return draftAnswer; } }

    private Question(QuestionId id, string subject, string question, string askedBy) : base()
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
        }

        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException($"'{nameof(question)}' cannot be null or whitespace.", nameof(question));
        }

        if (string.IsNullOrWhiteSpace(askedBy))
        {
            throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or whitespace.", nameof(askedBy));
        }

        RaiseEvent(new QuestionRecievedEvent(Id, subject, question, askedBy, DateTime.Now));
    }

    internal void Apply(QuestionRecievedEvent @event)
    {
        subject = @event.Subject;
        question = @event.Question;
        askedBy = @event.AskedBy;
        asked = @event.Asked;
    }

    public void Answer(string answer, string answeredBy)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        if (string.IsNullOrWhiteSpace(answeredBy))
        {
            throw new ArgumentException($"'{nameof(answeredBy)}' cannot be null or whitespace.", nameof(answeredBy));
        }

        if (DraftAnswer != null)
        {
            throw new AnswerQuestionsException("Question is already answered.");
        }

        RaiseEvent(new QuestionAnsweredEvent(Id, answer, answeredBy, DateTime.Now));
    }

    internal void Apply(QuestionAnsweredEvent @event)
    {
        draftAnswer = new DraftAnswer(this.Id, RaiseEvent);
        draftAnswer.Apply(@event);
    }

    internal void Apply(AnswerAcceptedEvent @event) => draftAnswer.Apply(@event);

    internal void Apply(AnswerRejectedEvent @event) => draftAnswer.Apply(@event);

    internal void Apply(AnswerModifiedEvent @event) => draftAnswer.Apply(@event);

    internal void Apply(AnswerSentEvent @event) => draftAnswer.Apply(@event);

    public QuestionAnswerdIntegrationEvent IsAnswered()
    {
        throw new NotImplementedException();
    }

    public void Revoke(string revokedBy)
    {
        throw new NotImplementedException();
    }
}
