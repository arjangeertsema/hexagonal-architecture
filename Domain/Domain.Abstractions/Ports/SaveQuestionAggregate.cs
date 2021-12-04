namespace Domain.Abstractions.Ports;

public class SaveQuestionAggregate : Command
{
    public IQuestion Question { get; }

    public SaveQuestionAggregate(IQuestion question) : base(Guid.NewGuid())
    {
        this.Question = question ?? throw new ArgumentNullException(nameof(question));
    }
}
