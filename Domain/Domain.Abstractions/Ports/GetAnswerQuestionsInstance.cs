using System;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestionsInstance : IQuery<GetAnswerQuestionsInstance.Response>
    {
        public GetAnswerQuestionsInstance(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; }

        public class Response
        {

        }
    }
}