namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestion : IQuery<GetAnswerQuestion.Response>
    {
        public GetAnswerQuestion(AnswerQuestionId questionId)
        {
            QuestionId = questionId;
        }

        public AnswerQuestionId QuestionId { get; }

        public class Response
        {

        }
    }
}