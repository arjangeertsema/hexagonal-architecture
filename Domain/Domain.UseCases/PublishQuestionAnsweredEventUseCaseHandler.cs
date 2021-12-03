namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class PublishQuestionAnsweredEventUseCaseHandler : ICommandHandler<PublishQuestionAnsweredEventUseCase>
{
    private readonly IMediator mediator;
    private readonly IQuestionService questionService;

    public PublishQuestionAnsweredEventUseCaseHandler(IMediator mediator, IQuestionService questionService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
    }
    
    [HasPermission("PUBLISH_QUESTION_ANSWERED_EVENT")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(PublishQuestionAnsweredEventUseCase command, CancellationToken cancellationToken)
    {
        var question = await questionService.Get(command.QuestionId, cancellationToken);

        var @event = question.IsAnswered();

        await questionService.Save(question, cancellationToken);
        await mediator.Send(new PublishEvent<QuestionAnswerdIntegrationEvent>(@event));
    }
}
