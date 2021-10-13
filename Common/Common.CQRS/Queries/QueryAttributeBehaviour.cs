using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Queries;
using Common.CQRS.Abstractions.Attributes;

namespace Common.CQRS.Queries
{
    public class QueryAttributeBehaviour<TQuery, TResponse> : AttributeBehaviour, IQueryBehaviour<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type queryBehaviourType;

        public QueryAttributeBehaviour(IServiceProvider serviceProvider) 
            : base()
        {   
            this.queryBehaviourType = typeof(IQueryAttributeBehaviour<,,>);
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task<TResponse> Handle(TQuery query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next)
        {
            Task<TResponse> nextDelegate() => next();

            var pipeline = this.GetBehaviourAttributes(attributeCollection)
                .Select(a => new { 
                    Attribute = a, 
                    Behaviour = GetQueryAttributeBehaviour(a) 
                })
                .Aggregate(
                    seed: (QueryBehaviourDelegate<TResponse>) nextDelegate, 
                    func: (nxt, item) => ()  => item.Behaviour.Handle(query, item.Attribute, cancellationToken, nxt)
                );

            return pipeline();
        }

        private IQueryAttributeBehaviour<TQuery, TResponse, BehaviourAttribute> GetQueryAttributeBehaviour(BehaviourAttribute attribute)
        {
            return (IQueryAttributeBehaviour<TQuery, TResponse, BehaviourAttribute>) this.serviceProvider.GetRequiredService
            (
                serviceType: this.queryBehaviourType.MakeGenericType(typeof(TQuery), typeof(TResponse), attribute.GetType())
            );
        }
    }
}