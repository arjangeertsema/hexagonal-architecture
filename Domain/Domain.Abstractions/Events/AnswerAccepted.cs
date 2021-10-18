using System;
using Common.DDD.Abstractions;
using Common.UserTasks.Abstractions;

namespace Domain.Abstractions.Events
{
    public class AnswerAcceptedEvent : DomainEvent, IUserTaskId
    {
        public AnswerAcceptedEvent(Guid aggregateId, string userTaskId, string acceptedBy, DateTime accepted)
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

        public string UserTaskId { get; }
        public string AcceptedBy { get; }
        public DateTime Accepted { get; }
    }
}