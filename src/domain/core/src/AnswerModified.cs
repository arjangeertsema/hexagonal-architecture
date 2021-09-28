using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    internal class AnswerModifiedEvent : DomainEvent<QuestionId>
    {
        public AnswerModifiedEvent(QuestionId aggregateId, string answer, string modifiedBy)
            : base(aggregateId)
        {
            Answer = answer;
            ModifiedBy = modifiedBy;
            Modified = DateTime.Now;
        }

        public AnswerModifiedEvent(QuestionId aggregateId, long aggregateVersion, string answer, string modifiedBy, DateTime modified)
            : base(aggregateId, aggregateVersion)

        {
            Answer = answer;
            ModifiedBy = modifiedBy;
            Modified = modified;
        }

        public string Answer { get; }
        public string ModifiedBy { get; }
        public DateTime Modified { get; }

        public override IDomainEvent<QuestionId> WithAggregate(QuestionId aggregateId, long aggregateVersion)
        {
            return new AnswerModifiedEvent(aggregateId, aggregateVersion, Answer, ModifiedBy, Modified);
        }
    }
}