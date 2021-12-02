namespace Domain.Abstractions.UseCases;

public class GetQuestionsUseCase : IQuery<IEnumerable<GetQuestionsUseCase.Response>>
{
    public GetQuestionsUseCase(int offset, int limit)
    {
        Offset = offset;
        Limit = limit;
    }

    public int Offset { get; }
    public int Limit { get; }

    public class Response
    {
        public Response(AnswerQuestionId questionId, string subject, string question, DateTime asked, string askedBy, DateTime lastActivity, AnswerQuestionStatus status)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or empty.", nameof(subject));
            }

            if (string.IsNullOrEmpty(question))
            {
                throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
            }

            if (string.IsNullOrEmpty(askedBy))
            {
                throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or empty.", nameof(askedBy));
            }

            QuestionId = questionId;
            Subject = subject;
            Question = question;
            Asked = asked;
            AskedBy = askedBy;            
            LastActivity = lastActivity;
            Status = status;
        }

        public AnswerQuestionId QuestionId { get; }
        public string Subject { get; }
        public string Question { get; }
        public DateTime Asked { get; }
        public string AskedBy { get; }
        public DateTime LastActivity { get; }
        public AnswerQuestionStatus Status { get; }
    }
}
