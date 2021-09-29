using System;
using Reference.Domain.Abstractions.DDD;

namespace Reference.Domain.Core.AnswerQuestions
{
    public class QuestionAnsweredEvent : DomainEvent
    {
        public QuestionAnsweredEvent(Guid aggregateId, string answer, string answeredBy, DateTime answered)
            : base(aggregateId)

        {
            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = answered;
        }

        public string Answer { get; }
        public string AnsweredBy { get; }
        public DateTime Answered { get; }
    }
}