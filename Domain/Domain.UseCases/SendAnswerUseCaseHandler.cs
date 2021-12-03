namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class SendAnswerUseCaseHandler : ICommandHandler<SendAnswerUseCase>
{
    private readonly IMediator mediator;
    private readonly IQuestionService questionService;

    public SendAnswerUseCaseHandler(IMediator mediator, IQuestionService questionService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }
    
    [HasPermission("SEND_ANSWER")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(SendAnswerUseCase command, CancellationToken cancellationToken)
    {
        var question = await questionService.Get(command.QuestionId, cancellationToken);

        var message = question.DraftAnswer.Send();

        await questionService.Save(question, cancellationToken);
        await mediator.Send(new SendMessage(message));
    }
}
