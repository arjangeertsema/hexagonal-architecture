namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class ModifyAnswerUseCaseHandler : ICommandHandler<ModifyAnswerUseCase>
{
    private readonly IMediator mediator;
    private readonly IQuestionService questionService;

    public ModifyAnswerUseCaseHandler(IMediator mediator, IQuestionService questionService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }
    
    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(ModifyAnswerUseCase command, CancellationToken cancellationToken)
    {
        var (question, userId) = await TaskUtil.WhenAll
        (
            questionService.Get(command.QuestionId, cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.DraftAnswer.Modify(command.Answer, userId);

        await questionService.Save(question, cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId), cancellationToken);
    }
}
