using System;
using System.Collections.Generic;

namespace Reference.Domain.Abstractions.Ports.Input
{

    public class GetQuestionsUseCase : IInputPort<GetQuestionsUseCase.Response>
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
                Items = items;
            }
            public IEnumerable<Response.Item> Items { get; }

            public class Item
            {

                public Item(Guid questionId, string subject, string question, string askedBy, DateTime askedOn, DateTime lastActivityOn, int status)
                {
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
