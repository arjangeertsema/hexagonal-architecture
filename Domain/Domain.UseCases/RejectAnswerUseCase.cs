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
        var aggregateRootTask = mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);
        var userIdTask = mediator.Ask(new GetUserId(), cancellationToken);
        await Task.WhenAll(aggregateRootTask, userIdTask);

        aggregateRootTask.Result.RejectAnswer
        (
            userTaskId: command.UserTaskId,
            rejection: command.Rejection,
            rejectedBy: userIdTask.Result
        );

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRootTask.Result), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId, new { ReviewResult = "Accepted" }), cancellationToken);        
    }
}
