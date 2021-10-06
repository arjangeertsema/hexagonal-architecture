using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Queries;

namespace Synion.CQRS.Queries
{
    internal class BehaviourQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IServiceProvider serviceProvider;

        public BehaviourQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
            var attributes = GetHandlerAttributes(handler);
            Task<TResponse> handleDelegate() => handler.Handle(query, cancellationToken);

            var pipeline = serviceProvider
                .GetServices<IQueryBehaviour<TQuery, TResponse>>()
                .Reverse()
                .Aggregate(
                    seed: (QueryBehaviourDelegate<TResponse>) handleDelegate, 
                    func: (next, behaviour) => () => behaviour.Handle(query, attributes, cancellationToken, next)
                );
                
            return pipeline();
        }

        private static IAttributeCollection GetHandlerAttributes(IQueryHandler<TQuery, TResponse> handler) 
        {
            var name = nameof(IQueryHandler<TQuery, TResponse>.Handle);
            var reference = typeof(IQueryHandler<TQuery, TResponse>)
                .GetMethods()
                .Where(m => m.Name.Equals(name))
                .Single();

            var methodInfo = handler.GetType().GetMethod
            (
                name: reference.Name,
                types: reference.GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray()
            );

            if(methodInfo == null)
                throw new MissingMethodException(handler.GetType().FullName, reference.Name);

            var attributes = System.Attribute.GetCustomAttributes(methodInfo);
            return new AttributeCollection(attributes);
        }
    }
}