using System;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestionPort : IQuery<GetAnswerQuestionPort.Response>
    {
        public GetAnswerQuestionPort(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; }

        public class Response
        {

        }
    }
}