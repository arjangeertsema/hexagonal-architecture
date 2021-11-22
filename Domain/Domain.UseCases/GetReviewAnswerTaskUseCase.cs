namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetReviewAnswerTaskUseCaseHandler : IQueryHandler<GetReviewAnswerTaskUseCase, GetReviewAnswerTaskUseCase.Response>
{
    private readonly IMediator mediator;

    public GetReviewAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("REVIEW_ANSWER")]
    [IsUserTaskOwner]
    public async Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query, CancellationToken cancellationToken)
    {
        var task = await mediator.Ask(new GetUserTask(query.UserTaskId));
        var questionId = new AnswerQuestionId(task.ReferenceId);
        var instance = await mediator.Ask(new GetAnswerQuestion(questionId));

        return Map(task, instance);
    }

    private GetReviewAnswerTaskUseCase.Response Map(GetUserTask.Response task, GetAnswerQuestion.Response instance)
    {
        throw new NotImplementedException();
    }
}
