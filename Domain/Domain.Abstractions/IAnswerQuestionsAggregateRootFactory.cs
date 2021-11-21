namespace Domain.Abstractions;

public interface IAnswerQuestionsAggregateRootFactory
{
    IAnswerQuestionsAggregateRoot Create(Guid questionId, string subject, string question, string askedBy);
}
