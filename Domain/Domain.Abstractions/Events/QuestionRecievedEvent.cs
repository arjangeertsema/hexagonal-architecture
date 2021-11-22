namespace Domain.Abstractions.Events;

public class QuestionRecievedEvent : VersionedDomainEvent<AnswerQuestionId>
{
    public QuestionRecievedEvent(AnswerQuestionId aggregateId, string subject, string question, string askedBy, DateTime asked)
        : base(aggregateId)
    {
        Subject = subject;
        Question = question;
        AskedBy = askedBy;
        Asked = asked;
    }

    public string Subject { get; }
    public string Question { get; }
    public string AskedBy { get; }
    public DateTime Asked { get; }
}
