using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Core.AnswerQuestions
{
    public class AnswerAcceptedEvent : DomainEvent
    {
        public AnswerAcceptedEvent(Guid aggregateId, string acceptedBy, DateTime accepted)
            : base(aggregateId)
        {
            AcceptedBy = acceptedBy;
            Accepted = accepted;
        }
        
        public string AcceptedBy { get; }
        public DateTime Accepted { get; }
    }
}