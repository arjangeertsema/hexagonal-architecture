namespace Domain.Abstractions.UseCases;

public class AskQuestionUseCase : Command
{
    public AskQuestionUseCase(Guid commandId, QuestionId questionId, string subject, string question, string askedBy) : base(commandId)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
        }

        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
        }

        if (string.IsNullOrWhiteSpace(askedBy))
        {
            throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or empty.", nameof(askedBy));
        }

        QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
        Subject = subject;
        Question = question;
        AskedBy = askedBy;
    }

    public QuestionId QuestionId { get; }
    public string Subject { get; }
    public string Question { get; }
    public string AskedBy { get; }
}
