using System;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;

namespace Domain.Abstractions.Events
{
    public class AnswerModifiedEvent : DomainEvent, IUserTask
    {
        public AnswerModifiedEvent(Guid aggregateId, long userTaskId, string answer, string modifiedBy, DateTime modified)
            : base(aggregateId)

        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            if (string.IsNullOrWhiteSpace(modifiedBy))
            {
                throw new ArgumentException($"'{nameof(modifiedBy)}' cannot be null or whitespace.", nameof(modifiedBy));
            }

            UserTaskId = userTaskId;
            Answer = answer;
            ModifiedBy = modifiedBy;
            Modified = modified;
        }

        public long UserTaskId { get; }
        public string Answer { get; }
        public string ModifiedBy { get; }
        public DateTime Modified { get; }
    }
}