namespace Domain.Core;

public class AnswerQuestionsAggregateRootFactory : IAnswerQuestionsAggregateRootFactory
{
    public IAnswerQuestionsAggregateRoot Create(Guid questionId, string subject, string question, string askedBy)
        => AnswerQuestionsAggregateRoot.Start(questionId, subject, question, askedBy);
}
