namespace Domain.Abstractions.Ports;

public class GetQuestions : IQuery<IEnumerable<GetQuestions.Response>>
{
    public GetQuestions()
    { }

    public GetQuestions(int limit, int offset)
    {
        Limit = limit;
        Offset = offset;
    }

    public int Limit { get; }
    public int Offset { get; }

    public class Response : GetQuestion.Response
    {
        public Response(QuestionId questionId, DateTime asked, string askedBy, string subject, string question, string answer, string rejection, DateTime lastActivity, AnswerQuestionStatus status)
            : base(questionId, asked, askedBy, subject, question, answer, rejection, lastActivity, status) { }
    }
}
