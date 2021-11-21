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
        var questionId = new AnswerQuestionId(command.QuestionId);
        var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(questionId), cancellationToken);

        var userId = await mediator.Ask(new GetUserId(), cancellationToken);

        aggregateRoot.AnswerQuestion
        (
            userTaskId: command.UserTaskId,
            answer: command.Answer,
            answeredBy: userId
        );

        await mediator.Send(new CompleteUserTask(command.CommandId, command.UserTaskId), cancellationToken);
        await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot, AnswerQuestionId>(command.CommandId, aggregateRoot), cancellationToken);
    }
}
