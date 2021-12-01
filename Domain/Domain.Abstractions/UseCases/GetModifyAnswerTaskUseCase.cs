namespace Domain.Abstractions.UseCases;

public class GetModifyAnswerTaskUseCase : IQuery<GetModifyAnswerTaskUseCase.Response>, IHasUserTask
{
    public GetModifyAnswerTaskUseCase(IUserTask userTask)
    {
        this.UserTask = userTask;
    }

    public IUserTask UserTask { get; }

    public class Response : IHasUserTaskClaim
    {
        public AnswerQuestionId QuestionId { get; }

        public Response(AnswerQuestionId questionId, IUserTaskClaim userTask, DateTime askedOn, string askedBy, string subject, string question, string answer, string rejection)
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

            if (string.IsNullOrEmpty(rejection))
            {
                throw new ArgumentException($"'{nameof(rejection)}' cannot be null or empty.", nameof(rejection));
            }

            QuestionId = questionId;
            UserTaskClaim = userTask;
            AskedOn = askedOn;
            AskedBy = askedBy;
            Subject = subject;
            Question = question;
            Answer = answer;
            Rejection = rejection;
        }

        public IUserTaskClaim UserTaskClaim { get; }
        public DateTime AskedOn { get; }
        public string Subject { get; }
        public string Question { get; }
        public string AskedBy { get; }
        public string Answer { get; }
        public string Rejection { get; }
    }
}
