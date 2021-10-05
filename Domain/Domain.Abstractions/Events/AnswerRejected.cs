using System;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;

namespace Domain.Abstractions.Events
{
    public class AnswerRejectedEvent : DomainEvent, IUserTask
    {
        public AnswerRejectedEvent(Guid aggregateId, long userTaskId, string rejection, string rejectedBy, DateTime rejected)
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

            UserTaskId = userTaskId;
            Rejection = rejection;
            RejectedBy = rejectedBy;
            Rejected = rejected;
        }

        public long UserTaskId { get; }
        public string Rejection { get; }
        public string RejectedBy { get; }
        public DateTime Rejected { get; }
    }
}