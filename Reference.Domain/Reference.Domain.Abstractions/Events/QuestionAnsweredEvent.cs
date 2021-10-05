using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Abstractions.Events
{
    public class QuestionAnsweredEvent : DomainEvent
    {
        public QuestionAnsweredEvent(Guid aggregateId, long taskId, string answer, string answeredBy, DateTime answered)
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

            TaskId = taskId;
            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = answered;
        }

        public long TaskId { get; }
        public string Answer { get; }
        public string AnsweredBy { get; }
        public DateTime Answered { get; }
    }
}