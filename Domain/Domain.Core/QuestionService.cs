namespace Domain.Core;

public class QuestionService : IQuestionService
{
    private readonly IMediator mediator;
    
    public QuestionService(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    public Task<IQuestion> Get(QuestionId questionId, CancellationToken cancellationToken)
        => mediator.Ask(new GetAggregateRoot<IQuestion, QuestionId>(questionId), cancellationToken);

    public Task Save(IQuestion question, CancellationToken cancellationToken)
        => mediator.Send(new SaveAggregateRoot<IQuestion, QuestionId>(question), cancellationToken);

    public IQuestion Create(QuestionId questionId, string subject, string question, string askedBy)
        => Question.Create(questionId, subject, question, askedBy);
}
