using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    internal class AnswerSentEvent : DomainEvent<QuestionId>
    {
        public AnswerSentEvent(QuestionId aggregateId)
            : base(aggregateId)
        {
            Sent = DateTime.Now;
        }

        public AnswerSentEvent(QuestionId aggregateId, long aggregateVersion, DateTime sent)
            : base(aggregateId, aggregateVersion)

        {
            Sent = sent;
        }

        public DateTime Sent { get; }

        public override IDomainEvent<QuestionId> WithAggregate(QuestionId aggregateId, long aggregateVersion)
        {
            return new AnswerSentEvent(aggregateId, aggregateVersion, Sent);
        }
    }
}