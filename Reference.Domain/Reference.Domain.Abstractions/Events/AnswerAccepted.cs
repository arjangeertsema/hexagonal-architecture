using System;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;

namespace Reference.Domain.Abstractions.Events
{
    public class AnswerAcceptedEvent : DomainEvent, IUserTask
    {
        public AnswerAcceptedEvent(Guid aggregateId, long userTaskId, string acceptedBy, DateTime accepted)
            : base(aggregateId)
        {
            if (string.IsNullOrWhiteSpace(acceptedBy))
            {
                throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
            }

            UserTaskId = userTaskId;
            AcceptedBy = acceptedBy;
            Accepted = accepted;
        }

        public long UserTaskId { get; }
        public string AcceptedBy { get; }
        public DateTime Accepted { get; }
    }
}