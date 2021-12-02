namespace Domain.Abstractions.UseCases;

public class GetAnswerQuestionTaskUseCase : IQuery<GetAnswerQuestionTaskUseCase.Response>, IHasUserTaskId
{
    public GetAnswerQuestionTaskUseCase(IUserTaskId userTaskId)
    {
        UserTaskId = userTaskId;
    }

    public IUserTaskId UserTaskId { get; }

    public class Response : IHasUserTaskClaim
    {
        public Response(AnswerQuestionId QuestionId, IUserTaskClaim userTaskClaim, DateTime askedOn, string askedBy, string subject, string question)
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

            this.QuestionId = QuestionId;
            this.UserTaskClaim = userTaskClaim;
            this.AskedOn = askedOn;
            this.AskedBy = askedBy;
            this.Subject = subject;
            this.Question = question;
        }

        public AnswerQuestionId QuestionId { get; }
        public IUserTaskClaim UserTaskClaim { get; }
        public DateTime AskedOn { get; }
        public string AskedBy { get; }
        public string Subject { get; }
        public string Question { get; }
    }
}
