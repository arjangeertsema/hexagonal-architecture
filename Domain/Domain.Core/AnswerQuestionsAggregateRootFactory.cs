namespace Domain.Core;

public class AnswerQuestionsAggregateRootFactory : IAnswerQuestionsAggregateRootFactory
{
    public IAnswerQuestionsAggregateRoot Create(AnswerQuestionId questionId, string subject, string question, string askedBy)
        => AnswerQuestionsAggregateRoot.Create(questionId, subject, question, askedBy);
}
