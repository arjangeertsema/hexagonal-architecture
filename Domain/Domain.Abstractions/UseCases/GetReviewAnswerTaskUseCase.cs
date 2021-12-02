namespace Domain.Abstractions.UseCases;

public class GetReviewAnswerTaskUseCase : IQuery<GetReviewAnswerTaskUseCase.Response>, IHasUserTaskId
{
    public GetReviewAnswerTaskUseCase(IUserTaskId userTaskId)
    {
        UserTaskId = userTaskId;
    }

    public IUserTaskId UserTaskId { get; }

    public class Response : IHasUserTaskClaim
    {
        public AnswerQuestionId QuestionId { get; }

        public Response(AnswerQuestionId questionId, IUserTaskClaim userTaskClaim, DateTime askedOn, string askedBy, string subject, string question, string answer)
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

            if (string.IsNullOrEmpty(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or empty.", nameof(answer));
            }

            QuestionId = questionId;
            UserTaskClaim = userTaskClaim;
            AskedOn = askedOn;
            AskedBy = askedBy;
            Subject = subject;
            Question = question;
            Answer = answer;
        }

        public IUserTaskClaim UserTaskClaim { get; }
        public DateTime AskedOn { get; }
        public string Subject { get; }
        public string Question { get; }
        public string AskedBy { get; }
        public string Answer { get; }
    }
}
