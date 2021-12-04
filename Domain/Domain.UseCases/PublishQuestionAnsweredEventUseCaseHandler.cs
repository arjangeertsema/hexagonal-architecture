namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class PublishQuestionAnsweredEventUseCaseHandler : ICommandHandler<PublishQuestionAnsweredEventUseCase>
{
    private readonly IMediator mediator;

    public PublishQuestionAnsweredEventUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [HasPermission("PUBLISH_QUESTION_ANSWERED_EVENT")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(PublishQuestionAnsweredEventUseCase command, CancellationToken cancellationToken)
    {
        var question = await mediator.Ask(new GetQuestionAggregate(command.QuestionId), cancellationToken);

        var @event = question.IsAnswered();

        await mediator.Send(new SaveQuestionAggregate(question), cancellationToken);
        await mediator.Send(new PublishEvent<QuestionAnswerdIntegrationEvent>(@event));
    }
}
