namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class RejectAnswerUseCaseHandler : ICommandHandler<RejectAnswerUseCase>
{
    private readonly IMediator mediator;
    private readonly IQuestionService questionService;

    public RejectAnswerUseCaseHandler(IMediator mediator, IQuestionService questionService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }
    
    [HasPermission("REVIEW_ANSWER")]
    [IsUserTaskOwner]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(RejectAnswerUseCase command, CancellationToken cancellationToken)
    {
        var (question, userId) = await TaskUtil.WhenAll
        (
            questionService.Get(command.QuestionId, cancellationToken),
            mediator.Ask(new GetUserId(), cancellationToken)
        );

        question.DraftAnswer.Reject(command.Rejection, userId);

        await questionService.Save(question, cancellationToken);
        await mediator.Send(new CompleteUserTask(command.UserTaskId, new KeyValuePair<string, object>("ReviewResult", "Rejected")), cancellationToken);        
    }
}
