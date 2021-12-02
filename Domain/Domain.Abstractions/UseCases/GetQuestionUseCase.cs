namespace Domain.Abstractions.UseCases;

public class GetQuestionUseCase : IQuery<GetQuestionUseCase.Response>
{
    public GetQuestionUseCase(AnswerQuestionId questionId)
    {
        this.QuestionId = questionId;
    }
    public AnswerQuestionId QuestionId { get; }

    public class Response : GetQuestionsUseCase.Response
    {
        public Response(AnswerQuestionId questionId, string subject, string question, DateTime asked, string askedBy, DateTime lastActivity, AnswerQuestionStatus status)
            : base(questionId, subject, question, asked, askedBy, lastActivity, status)
        { }
    }
}
