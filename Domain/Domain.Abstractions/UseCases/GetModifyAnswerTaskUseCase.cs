namespace Domain.Abstractions.UseCases;

public class GetModifyAnswerTaskUseCase : IQuery<GetModifyAnswerTaskUseCase.Response>, IHasUserTaskId
{
    public IUserTaskId UserTaskId { get; }

    public GetModifyAnswerTaskUseCase(IUserTaskId userTaskId)
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
        public string Rejection { get; }

        public Response(AnswerQuestionId questionId, IUserTaskId userTaskId, IUserTaskClaim userTaskClaim, DateTime askedOn, string askedBy, string subject, string question, string answer, string rejection)
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

            if (string.IsNullOrWhiteSpace(rejection))
            {
                throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
            }

            QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
            UserTaskId = userTaskId ?? throw new ArgumentNullException(nameof(userTaskId));
            UserTaskClaim = userTaskClaim ?? throw new ArgumentNullException(nameof(userTaskClaim));
            AskedOn = askedOn;
            AskedBy = askedBy;
            Subject = subject;
            Question = question;
            Answer = answer;
            Rejection = rejection;
        }
    }
}
