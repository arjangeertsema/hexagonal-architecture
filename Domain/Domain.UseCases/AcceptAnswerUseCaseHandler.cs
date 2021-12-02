namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class AcceptAnswerUseCaseHandler : ICommandHandler<AcceptAnswerUseCase>
{
    private readonly IMediator mediator;

    public AcceptAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("REVIEW_ANSWER")]
    [IsUserTaskOwner]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(AcceptAnswerUseCase command, CancellationToken cancellationToken)
    {
        var aggregateRootTask = mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);
        var userIdTask = mediator.Ask(new GetUserId(), cancellationToken);

        await Task.WhenAll(aggregateRootTask, userIdTask);

        aggregateRootTask.Result.AcceptAnswer
        (
            userTaskId: command.UserTaskId,
            acceptedBy: userIdTask.Result
        );
 
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRootTask.Result), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId, new KeyValuePair<string, object>("ReviewResult", "Accepted")), cancellationToken);
    }
}
