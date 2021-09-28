using System;
using System.Collections.Generic;

namespace example.domain.abstractions.ports.input
{
    public interface IGetQuestionsUseCase : IInputPort<IGetQuestionsUseCase.Query, IGetQuestionsUseCase.Response> 
    {
        public class Query : IQuery<Response>
        {
            public Query(int? offset, int? limit)
            {
                Offset = offset;
                Limit = limit;
            }

            public int? Offset { get; }
            public int? Limit { get; }            
        }

        public struct Response
        {
            public ICollection<Item> Items { get; set; }

            public struct Item
            {                
                public Guid QuestionId { get; set; }
                public string Subject { get; set; }
                public string Question { get; set; }            
                public string Sender { get; set; }
                public DateTime RecievedOn { get; set; }
                public DateTime LastActivityOn { get; set; }
                public int Status { get; set; }
            }
        }        
    }
}
