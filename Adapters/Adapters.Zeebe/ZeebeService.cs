namespace Adapters.Zeebe;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class ZeebeService :
    AnswerQuestionsHandlers,
    IDomainEventHandler<QuestionRecievedEvent, AnswerQuestionId>
{
    private const string QUESTION_RECIEVED_MESSAGE = "Message_QuestionRecieved_V1";
    private readonly IZeebeClient zeebeClient;
    private readonly IMediator mediator;
    private readonly ZeebeClientBootstrapOptions options;

    public ZeebeService(IZeebeClient zeebeClient, IMediator mediator, IOptions<ZeebeClientBootstrapOptions> options)
    {
        this.zeebeClient = zeebeClient ?? throw new ArgumentNullException(nameof(zeebeClient));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task Handle(QuestionRecievedEvent @event, CancellationToken cancellationToken)
    {
        if (@event is null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var state = new
        {
            @event.AggregateRootId,
            SendAnswerCommandId = Guid.NewGuid(),
            SendQuestionAnsweredEventCommandId = Guid.NewGuid()
        };

        //TODO: create own builder to enforce company start process policy.
        await zeebeClient.NewPublishMessageCommand()
            .MessageName(QUESTION_RECIEVED_MESSAGE)
            .CorrelationKey(@event.AggregateRootId.ToString())
            .MessageId(@event.EventId.ToString())
            .State(state)
            .Send(options.Worker.RetryTimeout, cancellationToken);
    }

    public override async Task HandleJob(SendAnswerJobV1 job, CancellationToken cancellationToken)
    {
        await mediator.Send(new AuthenticateSystem(), cancellationToken);

        var command = new SendAnswerUseCase
        (
            commandId: job.State.SendAnswerCommandId,
            questionId: job.State.QuestionId
        );

        await this.mediator.Send(command, cancellationToken);
    }

    public override async Task HandleJob(SendQuestionAnsweredEventJobV1 job, CancellationToken cancellationToken)
    {
        await mediator.Send(new AuthenticateSystem(), cancellationToken);

        var command = new PublishQuestionAnsweredEventUseCase
        (
            commandId: job.State.SendQuestionAnsweredEventCommandId,
            questionId: job.State.QuestionId
        );

        await mediator.Send(command, cancellationToken);
    }
}
