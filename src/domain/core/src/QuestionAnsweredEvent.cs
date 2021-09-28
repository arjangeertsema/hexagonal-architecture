using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    internal class QuestionAnsweredEvent : DomainEvent<QuestionId>
    {
        public QuestionAnsweredEvent(QuestionId aggregateId, string answer, string answeredBy)
            : base(aggregateId)
        {
            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = DateTime.Now;
        }

        public QuestionAnsweredEvent(QuestionId aggregateId, long aggregateVersion, string answer, string answeredBy, DateTime answered)
            : base(aggregateId, aggregateVersion)

        {
            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = answered;
        }

        public string Answer { get; }
        public string AnsweredBy { get; }
        public DateTime Answered { get; }

        public override IDomainEvent<QuestionId> WithAggregate(QuestionId aggregateId, long aggregateVersion)
        {
            return new QuestionAnsweredEvent(aggregateId, aggregateVersion, Answer, AnsweredBy, Answered);
        }
    }
}