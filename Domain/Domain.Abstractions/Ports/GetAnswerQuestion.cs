namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestion : IQuery<GetAnswerQuestion.Response>
    {
        public AnswerQuestionId QuestionId { get; }

        public GetAnswerQuestion(AnswerQuestionId questionId)
        {
            QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
        }

        public class Response
        {
            public AnswerQuestionId QuestionId { get; }
            public DateTime Asked { get; }
            public string AskedBy { get; }
            public string Subject { get; }
            public string Question { get; }
            public string Answer { get; }
            public string Rejection { get; }
            public DateTime LastActivity { get; }
            public AnswerQuestionStatus Status { get; }

            public Response(AnswerQuestionId questionId, DateTime asked, string askedBy, string subject, string question, string answer, string rejection, DateTime lastActivity, AnswerQuestionStatus status)
            {
                if (string.IsNullOrWhiteSpace(askedBy))
                {
                    throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or whitespace.", nameof(askedBy));
                }

                if (string.IsNullOrWhiteSpace(subject))
                {
                    throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
                }

                if (string.IsNullOrWhiteSpace(question))
                {
                    throw new ArgumentException($"'{nameof(question)}' cannot be null or whitespace.", nameof(question));
                }

                this.QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
                this.Asked = asked;
                this.AskedBy = askedBy;
                this.Subject = subject;
                this.Question = question;
                this.Answer = answer;
                this.Rejection = rejection;
                this.LastActivity = lastActivity;
                this.Status = status;
            }
        }
    }
}