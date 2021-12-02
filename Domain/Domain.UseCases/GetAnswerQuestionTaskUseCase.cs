namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetAnswerQuestionTaskUseCaseHandler : IQueryHandler<GetAnswerQuestionTaskUseCase, GetAnswerQuestionTaskUseCase.Response>
{
    private readonly IMediator mediator;

    public GetAnswerQuestionTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    public async Task<GetAnswerQuestionTaskUseCase.Response> Handle(GetAnswerQuestionTaskUseCase query, CancellationToken cancellationToken)
    {
        var task = await mediator.Ask(new GetUserTask(query.UserTaskId));
        var questionId = new AnswerQuestionId(task.ReferenceId);
        var instance = await mediator.Ask(new GetAnswerQuestion(questionId));

        return Map(task, instance);
    }

    private GetAnswerQuestionTaskUseCase.Response Map(GetUserTask.Response task, GetAnswerQuestion.Response instance)
    {
        throw new NotImplementedException();
    }
}
