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
        var (question, userId) = await TaskUtil.WhenAll
        (
            mediator.Ask(new GetQuestionAggregate(command.QuestionId), cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.DraftAnswer.Accept(userId);

        await mediator.Send(new SaveQuestionAggregate(question), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId, new KeyValuePair<string, object>("ReviewResult", "Accepted")), cancellationToken);
    }
}
