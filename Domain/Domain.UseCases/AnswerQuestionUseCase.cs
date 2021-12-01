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
        var aggregateRootTask = mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);
        var userIdTask = mediator.Ask(new GetUserId(), cancellationToken);

        await Task.WhenAll(aggregateRootTask, userIdTask);

        aggregateRootTask.Result.AnswerQuestion
        (
            userTask: command.UserTask,
            answer: command.Answer,
            answeredBy: userIdTask.Result
        );

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRootTask.Result), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTask), cancellationToken);
    }
}
