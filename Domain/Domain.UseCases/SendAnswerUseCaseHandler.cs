namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class SendAnswerUseCaseHandler : ICommandHandler<SendAnswerUseCase>
{
    private readonly IMediator mediator;

    public SendAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [HasPermission("SEND_ANSWER")]
    [Transactional]
    [MakeIdempotent]
    public async Task Handle(SendAnswerUseCase command, CancellationToken cancellationToken)
    {
        var question = await mediator.Ask(new GetQuestionAggregate(command.QuestionId), cancellationToken);

        var message = question.DraftAnswer.Send();

        await mediator.Send(new SaveQuestionAggregate(question), cancellationToken);
        await mediator.Send(new SendMessage(message));
    }
}
