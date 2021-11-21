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
        var questionId = new AnswerQuestionId(command.QuestionId);
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(questionId), cancellationToken);

        aggregateRoot.SendQuestionAnsweredEvent();

        await mediator.Send(new PublishEvent<QuestionAnswerdIntegrationEvent>(command.CommandId, new QuestionAnswerdIntegrationEvent(command.QuestionId)));
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.CommandId, aggregateRoot), cancellationToken);
    }
}