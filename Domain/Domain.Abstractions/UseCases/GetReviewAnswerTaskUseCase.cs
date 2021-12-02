namespace Domain.Abstractions.UseCases;

public class GetReviewAnswerTaskUseCase : IQuery<GetReviewAnswerTaskUseCase.Response>, IHasUserTaskId
{
    public IUserTaskId UserTaskId { get; }

    public GetReviewAnswerTaskUseCase(IUserTaskId userTaskId)
    {
        UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
    }

    public class Response : IHasUserTaskId, IHasUserTaskClaim
    {
        public AnswerQuestionId QuestionId { get; }
        public IUserTaskId UserTaskId { get; }
        public IUserTaskClaim UserTaskClaim { get; }
        public DateTime AskedOn { get; }
        public string Subject { get; }
        public string Question { get; }
        public string AskedBy { get; }
        public string Answer { get; }

        public Response(AnswerQuestionId questionId, IUserTaskId userTaskId, IUserTaskClaim userTaskClaim, DateTime askedOn, string askedBy, string subject, string question, string answer)
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

            QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
            UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
            UserTaskClaim = userTaskClaim ?? throw new ArgumentNullException(nameof(userTaskClaim));
            AskedOn = askedOn;
            AskedBy = askedBy;
            Subject = subject;
            Question = question;
            Answer = answer;
        }
    }
}
