namespace Domain.Abstractions.UseCases;

public class GetQuestionUseCase : IQuery<GetQuestionUseCase.Response>
{
    public GetQuestionUseCase(AnswerQuestionId questionId)
    {
        this.QuestionId = questionId;
    }
    public AnswerQuestionId QuestionId { get; }

    public class Response : GetQuestionsUseCase.Response.Item
    {
        public Response(AnswerQuestionId questionId, string subject, string question, string askedBy, DateTime askedOn, DateTime lastActivityOn, int status)
            : base(questionId, subject, question, askedBy, askedOn, lastActivityOn, status)
        { }
    }
}
