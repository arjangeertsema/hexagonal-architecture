namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestion : IQuery<GetAnswerQuestion.Response>
    {
        public GetAnswerQuestion(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; }

        public class Response
        {

        }
    }
}