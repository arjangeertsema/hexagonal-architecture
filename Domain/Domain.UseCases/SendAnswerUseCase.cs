namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class SendAnswerUseCaseHandler : ICommandHandler<SendAnswerUseCase>
{
    private readonly IMediator mediator;

    public SendAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("SEND_ANSWER")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(SendAnswerUseCase command, CancellationToken cancellationToken)
    {
        var questionId = new AnswerQuestionId(command.QuestionId);
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(questionId), cancellationToken);

        var message = aggregateRoot.SendAnswer();

        await mediator.Send(new SendMessage(command.CommandId, message));
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.CommandId, aggregateRoot), cancellationToken);
    }
}
