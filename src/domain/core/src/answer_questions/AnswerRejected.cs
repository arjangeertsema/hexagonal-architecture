using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Core.AnswerQuestions
{
    public class AnswerRejectedEvent : DomainEvent
    {
        public AnswerRejectedEvent(Guid aggregateId, string rejection, string rejectedBy, DateTime rejected)
            : base(aggregateId)

        {
            Rejection = rejection;
            RejectedBy = rejectedBy;
            Rejected = rejected;
        }

        public string Rejection { get; }
        public string RejectedBy { get; }
        public DateTime Rejected { get; }
    }
}