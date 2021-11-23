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
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);

        aggregateRoot.SendQuestionAnsweredEvent();

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRoot), cancellationToken);
        await mediator.Send(new PublishEvent<QuestionAnswerdIntegrationEvent>(new QuestionAnswerdIntegrationEvent(command.QuestionId)));        
    }
}
