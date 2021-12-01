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
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);

        var userId = await mediator.Ask(new GetUserId(), cancellationToken);

        aggregateRoot.AcceptAnswer
        (
            userTask: command.UserTask,
            acceptedBy: userId
        );
 
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRoot), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTask, new { ReviewResult = "Accepted" }), cancellationToken);
    }
}
