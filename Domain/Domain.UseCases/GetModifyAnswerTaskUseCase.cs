namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetModifyAnswerTaskUseCaseHandler : IQueryHandler<GetModifyAnswerTaskUseCase, GetModifyAnswerTaskUseCase.Response>
{
    private readonly IMediator mediator;

    public GetModifyAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    public async Task<GetModifyAnswerTaskUseCase.Response> Handle(GetModifyAnswerTaskUseCase query, CancellationToken cancellationToken)
    {
        var task = await mediator.Ask(new GetUserTask(query.UserTask));
        var questionId = new AnswerQuestionId(task.ReferenceId);
        var instance = await mediator.Ask(new GetAnswerQuestion(questionId));

        return Map(task, instance);
    }

    private GetModifyAnswerTaskUseCase.Response Map(GetUserTask.Response task, GetAnswerQuestion.Response instance)
    {
        throw new NotImplementedException();
    }
}
