namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetQuestionUseCaseHandler : 
    IQueryHandler<GetQuestionUseCase, GetQuestionUseCase.Response>,
    IQueryAuthorization<GetQuestionUseCase, GetQuestionUseCase.Response>
{
    private readonly IMediator mediator;

    public GetQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("GET_QUESTION")]
    [IsAuthorized]
    [ResponseNotDefault]
    public async Task<GetQuestionUseCase.Response> Handle(GetQuestionUseCase query, CancellationToken cancellationToken)
    {
        var question = await mediator.Ask(new GetQuestion(query.QuestionId));

        return Map(question);
    }

    public Task Authorize(string identity, GetQuestionUseCase query, GetQuestionUseCase.Response response)
    {
        throw new NotImplementedException();
    }

    private GetQuestionUseCase.Response Map(GetQuestion.Response answerQuestion)
    {
        return new GetQuestionUseCase.Response
        (
            questionId: answerQuestion.QuestionId,
            subject: answerQuestion.Subject,
            question: answerQuestion.Question,
            asked: answerQuestion.Asked,
            askedBy: answerQuestion.AskedBy,            
            lastActivity: answerQuestion.LastActivity,
            status: answerQuestion.Status
        );
    }
}
