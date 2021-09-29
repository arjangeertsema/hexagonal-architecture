using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Core.AnswerQuestions
{
    public class QuestionRegisteredEvent : DomainEvent
    {
        public QuestionRegisteredEvent(Guid aggregateId, string subject, string question, string askedBy, DateTime asked) 
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