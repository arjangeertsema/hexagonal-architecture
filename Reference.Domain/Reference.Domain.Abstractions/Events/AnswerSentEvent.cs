using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Abstractions.Events
{
    public class AnswerSentEvent : DomainEvent
    {

        public AnswerSentEvent(Guid aggregateId, DateTime sent)
            : base(aggregateId)

        {
            Sent = sent;
        }

        public DateTime Sent { get; }
    }
}