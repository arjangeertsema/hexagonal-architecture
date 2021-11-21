namespace Domain.Abstractions.UseCases;

public class RegisterQuestionUseCase : ICommand
{
    public RegisterQuestionUseCase(Guid commandId, Guid questionId, string subject, string question, string askedBy)
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

        CommandId = commandId;
        QuestionId = questionId;
        Subject = subject;
        Question = question;
        AskedBy = askedBy;
    }
    public Guid CommandId { get; }
    public Guid QuestionId { get; set; }
    public string Subject { get; }
    public string Question { get; }
    public string AskedBy { get; }
}
