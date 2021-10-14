using System;
using Common.DDD.Abstractions;
using Common.UserTasks.Abstractions;

namespace Domain.Abstractions.Events
{
    public class AnswerRejectedEvent : DomainEvent, IUserTask
    {
        public AnswerRejectedEvent(Guid aggregateId, string userTaskId, string rejection, string rejectedBy, DateTime rejected)
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

        public string UserTaskId { get; }
        public string Rejection { get; }
        public string RejectedBy { get; }
        public DateTime Rejected { get; }
    }
}