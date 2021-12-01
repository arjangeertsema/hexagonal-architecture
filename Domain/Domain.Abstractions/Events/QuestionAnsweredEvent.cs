namespace Domain.Abstractions.Events
{
    public class QuestionAnsweredEvent : VersionedDomainEvent<AnswerQuestionId>, IHasUserTask
    {
        public QuestionAnsweredEvent(AnswerQuestionId aggregateId, IUserTask userTask, string answer, string answeredBy, DateTime answered)
            : base(aggregateId)

        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            if (string.IsNullOrWhiteSpace(answeredBy))
            {
                throw new ArgumentException($"'{nameof(answeredBy)}' cannot be null or whitespace.", nameof(answeredBy));
            }

            UserTask = userTask;
            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = answered;
        }

        public IUserTask UserTask { get; }
        public string Answer { get; }
        public string AnsweredBy { get; }
        public DateTime Answered { get; }
    }
}