namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class EndQuestionUseCaseHandler : ICommandHandler<EndQuestionUseCase>
{
    private readonly IMediator mediator;

    public EndQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("END_QUESTION")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(EndQuestionUseCase command, CancellationToken cancellationToken)
    {
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);

        var userId = await mediator.Ask(new GetUserId(), cancellationToken);

        aggregateRoot.EndQuestion
        (
            endedBy: userId
        );

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.CommandId, aggregateRoot), cancellationToken);
    }
}
