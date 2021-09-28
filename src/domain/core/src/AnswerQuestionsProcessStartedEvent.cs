using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    public class AnswerQuestionsProcessStartedEvent : DomainEvent<QuestionId>
    {
        public AnswerQuestionsProcessStartedEvent(QuestionId aggregateId, string subject, string question, string askedBy) 
            : base(aggregateId)
        {
            Subject = subject;
            Question = question;
            AskedBy = askedBy;
            Asked = DateTime.Now;
        }

        public AnswerQuestionsProcessStartedEvent(QuestionId aggregateId, long aggregateVersion, string subject, string question, string askedBy, DateTime asked) 
            : base(aggregateId, aggregateVersion)
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

        public override IDomainEvent<QuestionId> WithAggregate(QuestionId aggregateId, long aggregateVersion)
        {
            return new AnswerQuestionsProcessStartedEvent(aggregateId, aggregateVersion, Subject, Question, AskedBy, Asked);
        }
    }
}