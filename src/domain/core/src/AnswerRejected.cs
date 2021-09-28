using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    internal class AnswerRejectedEvent : DomainEvent<QuestionId>
    {
        public AnswerRejectedEvent(QuestionId aggregateId, string rejection, string rejectedBy)
            : base(aggregateId)
        {
            Rejection = rejection;
            RejectedBy = rejectedBy;
            Rejected = DateTime.Now;
        }

        public AnswerRejectedEvent(QuestionId aggregateId, long aggregateVersion, string rejection, string rejectedBy, DateTime rejected)
            : base(aggregateId, aggregateVersion)

        {
            Rejection = rejection;
            RejectedBy = rejectedBy;
            Rejected = rejected;
        }

        public string Rejection { get; }
        public string RejectedBy { get; }
        public DateTime Rejected { get; }

        public override IDomainEvent<QuestionId> WithAggregate(QuestionId aggregateId, long aggregateVersion)
        {
            return new AnswerRejectedEvent(aggregateId, aggregateVersion, Rejection, RejectedBy, Rejected);
        }
    }
}