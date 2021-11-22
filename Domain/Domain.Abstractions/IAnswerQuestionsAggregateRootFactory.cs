namespace Domain.Abstractions;

public interface IAnswerQuestionsAggregateRootFactory
{
    IAnswerQuestionsAggregateRoot Create(AnswerQuestionId questionId, string subject, string question, string askedBy);
}
