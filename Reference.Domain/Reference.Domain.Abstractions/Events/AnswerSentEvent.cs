using System;
using Synion.DDD.Abstractions;

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