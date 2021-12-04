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
        var (question, userId) = await TaskUtil.WhenAll
        (
            mediator.Ask(new GetQuestionAggregate(command.QuestionId), cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.Answer(command.Answer, userId);

        await mediator.Send(new SaveQuestionAggregate(question), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId), cancellationToken);
    }
}
