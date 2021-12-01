namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class ModifyAnswerUseCaseHandler : ICommandHandler<ModifyAnswerUseCase>
{
    private readonly IMediator mediator;

    public ModifyAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(ModifyAnswerUseCase command, CancellationToken cancellationToken)
    {
        var aggregateRootTask = mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.QuestionId), cancellationToken);
        var userIdTask = mediator.Ask(new GetUserId(), cancellationToken);

        await Task.WhenAll(aggregateRootTask, userIdTask);

        aggregateRootTask.Result.ModifyAnswer
        (
            userTask: command.UserTask,
            answer: command.Answer,
            modifiedBy: userIdTask.Result
        );

        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(aggregateRootTask.Result), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTask), cancellationToken);
    }
}
