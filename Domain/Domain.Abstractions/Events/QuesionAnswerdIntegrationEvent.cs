using System;
using Common.CQRS.Abstractions;

namespace Domain.Abstractions.Events
{
    public class QuesionAnswerdIntegrationEvent : IEvent
    {
        public QuesionAnswerdIntegrationEvent(Guid questionId)
        {
            this.EventId = Guid.NewGuid();
            this.QuestionId = questionId;
        }

        public Guid EventId { get; }
        public Guid QuestionId { get; }
    }
}