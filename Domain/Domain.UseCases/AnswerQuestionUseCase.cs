namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class AnswerQuestionUseCaseHandler : ICommandHandler<AnswerQuestionUseCase>
{
    private readonly IMediator mediator;

    public AnswerQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(AnswerQuestionUseCase command, CancellationToken cancellationToken)
    {
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);

        var userId = await mediator.Ask(new GetUserId(), cancellationToken);

        aggregateRoot.AnswerQuestion
        (
            userTaskId: command.UserTaskId,
            answer: command.Answer,
            answeredBy: userId
        );

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRoot), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId), cancellationToken);
    }
}
