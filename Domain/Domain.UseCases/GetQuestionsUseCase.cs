namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetQuestionsUseCaseHandler : IQueryHandler<GetQuestionsUseCase, GetQuestionsUseCase.Response>
{
    private readonly IMediator mediator;

    public GetQuestionsUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("GET_QUESTION")]
    [IsAuthorized]
    public async Task<GetQuestionsUseCase.Response> Handle(GetQuestionsUseCase query, CancellationToken cancellationToken)
    {
        var instance = await mediator.Ask(new GetAnswerQuestions(query.Limit, query.Offset));

        return Map(instance);
    }

    private GetQuestionsUseCase.Response Map(GetAnswerQuestions.Response instance)
    {
        throw new NotImplementedException();
    }
}
