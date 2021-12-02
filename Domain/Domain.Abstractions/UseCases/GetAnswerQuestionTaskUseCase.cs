namespace Domain.Abstractions.UseCases;

public class GetAnswerQuestionTaskUseCase : IQuery<GetAnswerQuestionTaskUseCase.Response>, IHasUserTaskId
{
    public IUserTaskId UserTaskId { get; }
    public GetAnswerQuestionTaskUseCase(IUserTaskId userTaskId)
    {
        UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
    }

    public class Response : IHasUserTaskClaim
    {
        public Response(AnswerQuestionId questionId, IUserTaskId userTaskId, IUserTaskClaim userTaskClaim, DateTime asked, string askedBy, string subject, string question)
        {
            if (string.IsNullOrEmpty(askedBy))
            {
                throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or empty.", nameof(askedBy));
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or empty.", nameof(subject));
            }

            if (string.IsNullOrEmpty(question))
            {
                throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
            }

            this.QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
            this.UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
            this.UserTaskClaim = userTaskClaim ?? throw new ArgumentNullException(nameof(userTaskClaim));
            this.Asked = asked;
            this.AskedBy = askedBy;
            this.Subject = subject;
            this.Question = question;
        }

        public AnswerQuestionId QuestionId { get; }
        public IUserTaskId UserTaskId { get; }
        public IUserTaskClaim UserTaskClaim { get; }
        public DateTime Asked { get; }
        public string AskedBy { get; }
        public string Subject { get; }
        public string Question { get; }
    }
}
