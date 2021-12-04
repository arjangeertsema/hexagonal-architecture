namespace Domain.Abstractions.Ports;

public class CreateQuestionAggregate : Command
{
    public QuestionId QuestionId { get; }
    public string Subject { get; }
    public string Question { get; }
    public string AskedBy { get; }

    public CreateQuestionAggregate(QuestionId questionId, string subject, string question, string askedBy) 
        : base(Guid.NewGuid())
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

        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
        this.Subject = subject;
        this.Question = question;
        this.AskedBy = askedBy;
    }
}
