namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class EndQuestionUseCaseHandler : ICommandHandler<EndQuestionUseCase>
{
    private readonly IMediator mediator;
    private readonly IQuestionService questionService;

    public EndQuestionUseCaseHandler(IMediator mediator, IQuestionService questionService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }
    
    [HasPermission("END_QUESTION")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(EndQuestionUseCase command, CancellationToken cancellationToken)
    {
        var (question, userId) = await TaskUtil.WhenAll
        (
            questionService.Get(command.QuestionId, cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.Revoke(userId);

        await questionService.Save(question, cancellationToken);
    }
}
