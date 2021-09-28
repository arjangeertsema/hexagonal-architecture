using System.Collections.Generic;
using example.domain.abstractions.ddd;

namespace example.domain.abstractions.ports.output
{
    public interface IGetDomainEvents<TAggregateId> : IOutputPort<IGetDomainEvents<TAggregateId>.Query, IGetDomainEvents<TAggregateId>.Response>
    {
        public class Query : IQuery<Response>
        {   
            public Query(TAggregateId aggregateId)
            {
                AggregateId = aggregateId;
            }
            
            public TAggregateId AggregateId { get; }
        }

        public struct Response
        {            
            ICollection<IDomainEvent<TAggregateId>> Events { get; set; }
        }
    }
}