namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetQuestionUseCaseHandler : IQueryHandler<GetQuestionUseCase, GetQuestionUseCase.Response>
{
    private readonly IMediator mediator;

    public GetQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("GET_QUESTION")]
    [IsAuthorized]
    public async Task<GetQuestionUseCase.Response> Handle(GetQuestionUseCase query, CancellationToken cancellationToken)
    {
        var instance = await mediator.Ask(new GetAnswerQuestion(query.QuestionId));

        return Map(instance);
    }

    private GetQuestionUseCase.Response Map(GetAnswerQuestion.Response instance)
    {
        throw new NotImplementedException();
    }
}
