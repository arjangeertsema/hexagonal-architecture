namespace Adapters.EF;

public class EFService :
    IQueryHandler<GetAnswerQuestion, GetAnswerQuestion.Response>,
    IQueryHandler<GetAnswerQuestions, GetAnswerQuestions.Response>
{
    public Task<GetAnswerQuestion.Response> Handle(GetAnswerQuestion query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetAnswerQuestions.Response> Handle(GetAnswerQuestions query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
