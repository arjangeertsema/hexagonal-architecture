namespace Domain.Core;

public class QuestionService : 
    IQueryHandler<GetQuestionAggregate, IQuestion>,
    ICommandHandler<CreateQuestionAggregate>,
    ICommandHandler<SaveQuestionAggregate>
{
    private readonly IMediator mediator;
    
    public QuestionService(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    public Task Handle(CreateQuestionAggregate command, CancellationToken cancellationToken)
        => Task.FromResult(Question.Create(command.QuestionId, command.Subject, command.Question, command.AskedBy));

    public Task<IQuestion> Handle(GetQuestionAggregate query, CancellationToken cancellationToken)
        => mediator.Ask(new GetAggregateRoot<IQuestion, QuestionId>(query.QuestionId), cancellationToken);

    public Task Handle(SaveQuestionAggregate command, CancellationToken cancellationToken)
        => mediator.Send(new SaveAggregateRoot<IQuestion, QuestionId>(command.Question), cancellationToken);

    
}
