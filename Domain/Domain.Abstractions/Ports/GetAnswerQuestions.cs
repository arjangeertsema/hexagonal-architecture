namespace Domain.Abstractions.Ports;

public class GetAnswerQuestions : IQuery<IEnumerable<GetAnswerQuestions.Response>>
{
    public GetAnswerQuestions()
    { }

    public GetAnswerQuestions(int limit, int offset)
    {
        Limit = limit;
        Offset = offset;
    }

    public int Limit { get; }
    public int Offset { get; }

    public class Response : GetAnswerQuestion.Response
    {
        public Response(AnswerQuestionId questionId, DateTime asked, string askedBy, string subject, string question, string answer, string rejection, DateTime lastActivity, AnswerQuestionStatus status)
            : base(questionId, asked, askedBy, subject, question, answer, rejection, lastActivity, status) { }
    }
}
