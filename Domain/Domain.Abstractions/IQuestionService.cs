namespace Domain.Abstractions;

public interface IQuestionService
{
    IQuestion Create(QuestionId questionId, string subject, string question, string askedBy);
    Task<IQuestion> Get(QuestionId questionId, CancellationToken cancellationToken = default);
    Task Save(IQuestion question, CancellationToken cancellationToken = default);
}
