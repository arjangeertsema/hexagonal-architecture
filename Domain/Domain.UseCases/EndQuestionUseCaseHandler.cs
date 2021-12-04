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
        var (question, userId) = await TaskUtil.WhenAll
        (
            mediator.Ask(new GetQuestionAggregate(command.QuestionId), cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.Revoke(userId);

        await mediator.Send(new SaveQuestionAggregate(question), cancellationToken);
    }
}
