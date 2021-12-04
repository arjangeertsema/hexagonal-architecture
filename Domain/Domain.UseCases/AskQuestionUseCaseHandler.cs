﻿namespace UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class AskQuestionUseCaseHandler : ICommandHandler<AskQuestionUseCase>
{
    private readonly IMediator mediator;

    public AskQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ASK_QUESTION")]
    [Transactional]
    [MakeIdempotent]
    public Task Handle(AskQuestionUseCase command, CancellationToken cancellationToken)
    {
        return mediator.Send(new CreateQuestionAggregate(command.QuestionId, command.Subject, command.Question, command.AskedBy), cancellationToken);
    }
}