namespace Adapters.Zeebe;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class AnswerQuestionsService :
    IDomainEventHandler<QuestionRecievedEvent, AnswerQuestionId>,
    ICommandHandler<CompleteUserTask>,
    IAsyncJobHandler<SendAnswerJobV1>,
    IAsyncJobHandler<SendQuestionAnsweredEventJobV1>
{
    private const string QUESTION_RECIEVED_MESSAGE = "Message_QuestionRecieved_V1";
    private readonly IZeebeClient zeebeClient;
    private readonly IMediator mediator;

    public AnswerQuestionsService(IZeebeClient zeebeClient, IMediator mediator)
    {
        this.zeebeClient = zeebeClient ?? throw new ArgumentNullException(nameof(zeebeClient));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
            .Send(cancellationToken);
    }

    public Task Handle(CompleteUserTask command, CancellationToken cancellationToken)
    {
        var builder = this.zeebeClient.NewCompleteJobCommand(long.Parse(command.UserTaskId));

        if (command.State != null)
            builder = builder.State<object>(command.State);

        return builder.SendWithRetry(null, cancellationToken);
    }

    public async Task HandleJob(SendAnswerJobV1 job, CancellationToken cancellationToken)
    {
        await mediator.Send(new AuthenticateSystem(Guid.NewGuid()), cancellationToken);

        var command = new SendAnswerUseCase
        (
            commandId: job.State.SendAnswerCommandId,
            questionId: job.State.QuestionId
        );

        await this.mediator.Send(command, cancellationToken);
    }

    public async Task HandleJob(SendQuestionAnsweredEventJobV1 job, CancellationToken cancellationToken)
    {
        await mediator.Send(new AuthenticateSystem(Guid.NewGuid()), cancellationToken);

        var command = new PublishQuestionAnsweredEventUseCase
        (
            commandId: job.State.SendQuestionAnsweredEventCommandId,
            questionId: job.State.QuestionId
        );

        await mediator.Send(command, cancellationToken);
    }
}

public class SendAnswerJobV1 : AbstractJob<SendAnswerJobV1.JobState>
{
    public SendAnswerJobV1(IJob job, SendAnswerJobV1.JobState state) : base(job, state) { }

    public class JobState
    {
        public Guid QuestionId { get; set; }
        public Guid SendAnswerCommandId { get; set; }
    }
}

public class SendQuestionAnsweredEventJobV1 : AbstractJob<SendQuestionAnsweredEventJobV1.JobState>
{
    public SendQuestionAnsweredEventJobV1(IJob job, SendQuestionAnsweredEventJobV1.JobState state) : base(job, state) { }

    public class JobState
    {
        public Guid QuestionId { get; set; }
        public Guid SendQuestionAnsweredEventCommandId { get; set; }
    }
}
