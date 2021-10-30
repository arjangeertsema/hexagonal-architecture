using System;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestionsPort : IQuery<GetAnswerQuestionsPort.Response>
    {
        public GetAnswerQuestionsPort()
        { }

        public GetAnswerQuestionsPort(int? limit, int? offset)
        {
            Limit = limit;
            Offset = offset;
        }

        public int? Limit { get; }
        public int? Offset { get; }

        public class Response
        {

        }
    }
}