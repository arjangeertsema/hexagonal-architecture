using System;
using Common.CQRS.Abstractions;

namespace Domain.Abstractions.Events
{
    public class QuestionAnswerdIntegrationEvent : IEvent
    {
        public QuestionAnswerdIntegrationEvent(Guid questionId)
        {
            this.EventId = Guid.NewGuid();
            this.QuestionId = questionId;
        }

        public Guid EventId { get; }
        public Guid QuestionId { get; }
    }
}