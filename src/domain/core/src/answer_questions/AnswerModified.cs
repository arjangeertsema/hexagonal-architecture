using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Core.AnswerQuestions
{
    public class AnswerModifiedEvent : DomainEvent
    {
        public AnswerModifiedEvent(Guid aggregateId, string answer, string modifiedBy, DateTime modified)
            : base(aggregateId)

        {
            Answer = answer;
            ModifiedBy = modifiedBy;
            Modified = modified;
        }

        public string Answer { get; }
        public string ModifiedBy { get; }
        public DateTime Modified { get; }
    }
}