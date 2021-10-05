using System;
using Synion.DDD.Abstractions;

namespace Reference.Domain.Abstractions.Events
{
    public class AnswerAcceptedEvent : DomainEvent
    {
        public AnswerAcceptedEvent(Guid aggregateId, long taskId, string acceptedBy, DateTime accepted)
            : base(aggregateId)
        {
            if (string.IsNullOrWhiteSpace(acceptedBy))
            {
                throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
            }

            TaskId = taskId;
            AcceptedBy = acceptedBy;
            Accepted = accepted;
        }

        public long TaskId { get; }
        public string AcceptedBy { get; }
        public DateTime Accepted { get; }
    }
}