using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Abstractions.Events
{
    public class AnswerRejectedEvent : DomainEvent
    {
        public AnswerRejectedEvent(Guid aggregateId, long taskId, string rejection, string rejectedBy, DateTime rejected)
            : base(aggregateId)

        {
            if (string.IsNullOrWhiteSpace(rejection))
            {
                throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
            }

            if (string.IsNullOrWhiteSpace(rejectedBy))
            {
                throw new ArgumentException($"'{nameof(rejectedBy)}' cannot be null or whitespace.", nameof(rejectedBy));
            }

            TaskId = taskId;
            Rejection = rejection;
            RejectedBy = rejectedBy;
            Rejected = rejected;
        }

        public long TaskId { get; }
        public string Rejection { get; }
        public string RejectedBy { get; }
        public DateTime Rejected { get; }
    }
}