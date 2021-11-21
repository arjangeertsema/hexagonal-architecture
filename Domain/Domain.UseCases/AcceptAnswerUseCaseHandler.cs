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
        var questionId = new AnswerQuestionId(command.QuestionId);
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(questionId), cancellationToken);

        var userId = await mediator.Ask(new GetUserId(), cancellationToken);

        aggregateRoot.AcceptAnswer
        (
            userTaskId: command.UserTaskId,
            acceptedBy: userId
        );
 
        await mediator.Send(new CompleteUserTask(command.CommandId, command.UserTaskId, new { ReviewResult = "Accepted" }), cancellationToken);
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.CommandId, aggregateRoot), cancellationToken);
    }
}
