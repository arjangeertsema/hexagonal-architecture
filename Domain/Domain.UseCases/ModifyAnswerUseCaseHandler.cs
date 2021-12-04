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
        var (question, userId) = await TaskUtil.WhenAll
        (
            mediator.Ask(new GetQuestionAggregate(command.QuestionId), cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.DraftAnswer.Modify(command.Answer, userId);

        await mediator.Send(new SaveQuestionAggregate(question), cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId), cancellationToken);
    }
}
