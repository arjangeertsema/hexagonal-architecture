using System;
using Synion.DDD.Abstractions;

namespace Domain.Abstractions.Events
{
    public class QuestionRecievedEvent : DomainEvent
    {
        public QuestionRecievedEvent(Guid aggregateId, string subject, string question, string askedBy, DateTime asked) 
            : base(aggregateId)
        {
            Subject = subject;
            Question = question;
            AskedBy = askedBy;
            Asked = asked;
        }

        public string Subject { get; }
        public string Question { get; }
        public string AskedBy { get; }
        public DateTime Asked { get; }
    }
}