namespace Domain.Abstractions.UseCases;

public class GetQuestionUseCase : IQuery<GetQuestionUseCase.Response>
{
    public GetQuestionUseCase(QuestionId questionId)
    {
        this.QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
    }
    public QuestionId QuestionId { get; }

    public class Response : GetQuestionsUseCase.Response
    {
        public Response(QuestionId questionId, string subject, string question, DateTime asked, string askedBy, DateTime lastActivity, AnswerQuestionStatus status)
            : base(questionId, subject, question, asked, askedBy, lastActivity, status)
        { }
    }
}
