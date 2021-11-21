namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class RejectAnswerUseCaseHandler : ICommandHandler<RejectAnswerUseCase>
{
    private readonly IMediator mediator;

    public RejectAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("REVIEW_ANSWER")]
    [IsUserTaskOwner]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(RejectAnswerUseCase command, CancellationToken cancellationToken)
    {
        var questionId = new AnswerQuestionId(command.QuestionId);
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(questionId), cancellationToken);

        var userId = await mediator.Ask(new GetUserId(), cancellationToken);

        aggregateRoot.RejectAnswer
        (
            userTaskId: command.UserTaskId,
            rejection: command.Rejection,
            rejectedBy: userId
        );

        await mediator.Send(new CompleteUserTask(command.CommandId, command.UserTaskId, new { ReviewResult = "Accepted" }), cancellationToken);
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.CommandId, aggregateRoot), cancellationToken);
    }
}
