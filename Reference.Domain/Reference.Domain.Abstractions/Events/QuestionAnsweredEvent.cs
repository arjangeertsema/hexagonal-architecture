using System;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;

namespace Reference.Domain.Abstractions.Events
{
    public class QuestionAnsweredEvent : DomainEvent, IUserTask
    {
        public QuestionAnsweredEvent(Guid aggregateId, long userTaskId, string answer, string answeredBy, DateTime answered)
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

            UserTaskId = userTaskId;
            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = answered;
        }

        public long UserTaskId { get; }
        public string Answer { get; }
        public string AnsweredBy { get; }
        public DateTime Answered { get; }
    }
}