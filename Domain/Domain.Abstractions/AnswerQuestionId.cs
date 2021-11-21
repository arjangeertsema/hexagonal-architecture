namespace Domain.Abstractions;

public class AnswerQuestionId : IEntityId
{
    public Guid Id { get; }

    public AnswerQuestionId(Guid id)
    {
        this.Id = id;
    }
}
