using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    internal class AnswerAcceptedEvent : DomainEvent<QuestionId>
    {
        public AnswerAcceptedEvent(QuestionId aggregateId, string accepted)
            : base(aggregateId)
        {
            AcceptedBy = accepted;
            Accepted = DateTime.Now;
        }

        public AnswerAcceptedEvent(QuestionId aggregateId, long aggregateVersion, string acceptedBy, DateTime accepted)
            : base(aggregateId, aggregateVersion)

        {
            AcceptedBy = acceptedBy;
            Accepted = accepted;
        }
        public string AcceptedBy { get; }
        public DateTime Accepted { get; }

        public override IDomainEvent<QuestionId> WithAggregate(QuestionId aggregateId, long aggregateVersion)
        {
           return new AnswerAcceptedEvent(aggregateId, aggregateVersion, AcceptedBy, Accepted);
        }
    }
}