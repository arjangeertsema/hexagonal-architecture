using System;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports
{
    public class GetAnswerQuestionsInstances : IQuery<GetAnswerQuestionsInstances.Response>
    {
        public GetAnswerQuestionsInstances()
        { }

        public GetAnswerQuestionsInstances(int? limit, int? offset)
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