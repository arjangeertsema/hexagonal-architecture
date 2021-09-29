using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Core.AnswerQuestions
{
    public class AnswerModifiedEvent : DomainEvent
    {
        public AnswerModifiedEvent(Guid aggregateId, long taskId, string answer, string modifiedBy, DateTime modified)
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

            TaskId = taskId;
            Answer = answer;
            ModifiedBy = modifiedBy;
            Modified = modified;
        }

        public long TaskId { get; }
        public string Answer { get; }
        public string ModifiedBy { get; }
        public DateTime Modified { get; }
    }
}