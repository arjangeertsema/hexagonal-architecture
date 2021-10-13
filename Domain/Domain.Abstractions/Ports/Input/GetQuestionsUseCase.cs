using System;
using System.Collections.Generic;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports.Input
{

    public class GetQuestionsUseCase : IQuery<GetQuestionsUseCase.Response>
    {
        public GetQuestionsUseCase(int? offset, int? limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public int? Offset { get; }
        public int? Limit { get; }

        public class Response
        {

            public Response(IEnumerable<Response.Item> items)
            {
                Items = items ?? throw new ArgumentNullException(nameof(items));
            }
            public IEnumerable<Response.Item> Items { get; }

            public class Item
            {

                public Item(Guid questionId, string subject, string question, string askedBy, DateTime askedOn, DateTime lastActivityOn, int status)
                {
                    if (string.IsNullOrEmpty(subject))
                    {
                        throw new ArgumentException($"'{nameof(subject)}' cannot be null or empty.", nameof(subject));
                    }

                    if (string.IsNullOrEmpty(question))
                    {
                        throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
                    }

                    if (string.IsNullOrEmpty(askedBy))
                    {
                        throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or empty.", nameof(askedBy));
                    }

                    QuestionId = questionId;
                    Subject = subject;
                    Question = question;
                    AskedBy = askedBy;
                    AskedOn = askedOn;
                    LastActivityOn = lastActivityOn;
                    Status = status;
                }
                public Guid QuestionId { get; }
                public string Subject { get; }
                public string Question { get; }
                public string AskedBy { get; }
                public DateTime AskedOn { get; }
                public DateTime LastActivityOn { get; }
                public int Status { get; }
            }
        }
    }
}
