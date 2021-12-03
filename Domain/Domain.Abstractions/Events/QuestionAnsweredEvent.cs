namespace Domain.Abstractions.Events
{
    public class QuestionAnsweredEvent : VersionedDomainEvent<QuestionId>
    {
        public QuestionAnsweredEvent(QuestionId aggregateId, string answer, string answeredBy, DateTime answered)
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

            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = answered;
        }

        public string Answer { get; }
        public string AnsweredBy { get; }
        public DateTime Answered { get; }
    }
}