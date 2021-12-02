namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetQuestionsUseCaseHandler : 
    IQueryHandler<GetQuestionsUseCase, IEnumerable<GetQuestionsUseCase.Response>>,
    IQueryAuthorization<GetQuestionsUseCase, IEnumerable<GetQuestionsUseCase.Response>>
{
    private readonly IMediator mediator;

    public GetQuestionsUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("GET_QUESTION")]
    [IsAuthorized]
    public async Task<IEnumerable<GetQuestionsUseCase.Response>> Handle(GetQuestionsUseCase query, CancellationToken cancellationToken)
    {
        var answerQuestions = await mediator.Ask(new GetAnswerQuestions(query.Limit, query.Offset));

        return answerQuestions.Select(q => Map(q));
    }

    public Task Authorize(string identity, GetQuestionsUseCase query, IEnumerable<GetQuestionsUseCase.Response> response)
    {
        throw new NotImplementedException();
    }

    private GetQuestionsUseCase.Response Map(GetAnswerQuestions.Response answerQuestion)
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
