namespace UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class AskQuestionUseCaseHandler : ICommandHandler<AskQuestionUseCase>
{
    private readonly IMediator mediator;
    private readonly IAnswerQuestionsAggregateRootFactory aggregateRootFactory;

    public AskQuestionUseCaseHandler(IMediator mediator, IAnswerQuestionsAggregateRootFactory aggregateRootFactory)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.aggregateRootFactory = aggregateRootFactory ?? throw new ArgumentNullException(nameof(aggregateRootFactory));
    }

    [HasPermission("ASK_QUESTION")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(AskQuestionUseCase command, CancellationToken cancellationToken)
    {
        var aggregateRoot = aggregateRootFactory.Create
        (
            questionId: command.QuestionId,
            subject: command.Subject,
            question: command.Question,
            askedBy: command.AskedBy
        );

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRoot), cancellationToken);
    }
}
