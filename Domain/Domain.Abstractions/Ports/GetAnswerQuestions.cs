namespace Domain.Abstractions.Ports;

public class GetAnswerQuestions : IQuery<GetAnswerQuestions.Response>
{
    public GetAnswerQuestions()
    { }

    public GetAnswerQuestions(int? limit, int? offset)
    {
        Limit = limit;
        Offset = offset;
    }

    public int? Limit { get; }
    public int? Offset { get; }

    public class Response
    {

    }
}
