namespace Domain.Core;

public class AnswerQuestionsAggregateRootFactory : IAnswerQuestionsAggregateRootFactory
{
    public IAnswerQuestionsAggregateRoot Create(AnswerQuestionId questionId, string subject, string question, string askedBy)
        => AnswerQuestionsAggregateRoot.Start(questionId, subject, question, askedBy);
}
