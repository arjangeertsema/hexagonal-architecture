namespace UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class AskQuestionUseCaseHandler : ICommandHandler<AskQuestionUseCase>
{
    private readonly IMediator mediator;
    private readonly IQuestionService questionService;

    public AskQuestionUseCaseHandler(IMediator mediator, IQuestionService questionService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }

    [HasPermission("ASK_QUESTION")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(AskQuestionUseCase command, CancellationToken cancellationToken)
    {
        var question = questionService.Create
        (
            questionId: command.QuestionId,
            subject: command.Subject,
            question: command.Question,
            askedBy: command.AskedBy
        );

        await questionService.Save(question, cancellationToken);
    }
}
