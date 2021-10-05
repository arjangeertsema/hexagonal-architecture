using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace UseCases.Behaviours
{


    public abstract class AttributeBehaviour
    {
        private readonly Type behaviourAttributeType;

        public AttributeBehaviour()
        {
            this.behaviourAttributeType = typeof(BehaviourAttribute);            
        }

        protected IEnumerable<BehaviourAttribute> GetBehaviourAttributes(IEnumerable<Attribute> attributeCollection) 
        {
            return attributeCollection
                .Where(a => a.GetType().IsAssignableFrom(this.behaviourAttributeType))
                .Select(a => (BehaviourAttribute) a);
        }
    }

    public class CommandAttributeBehaviour<TCommand> : AttributeBehaviour, ICommandBehaviour<TCommand>
        where TCommand : ICommand
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type commandBehaviourType;

        public CommandAttributeBehaviour(IServiceProvider serviceProvider) 
            : base()
        {   
            this.commandBehaviourType = typeof(ICommandBehaviour<>);
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TCommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            Task nextDelegate() => next();

            return this.GetBehaviourAttributes(attributeCollection)
                .Select(a => 
                    new { 
                        Attribute = a, 
                        Behaviour = GetCommandAttributeBehaviour(a) 
                    }
                )
                .Aggregate(
                    seed: (CommandBehaviourDelegate) nextDelegate,
                    func: (n, i) => () => i.Behaviour.Handle(command, i.Attribute, cancellationToken, n)
                )();
        }

        private ICommandAttributeBehaviour<TCommand, BehaviourAttribute> GetCommandAttributeBehaviour(BehaviourAttribute attribute)
        {
            return (ICommandAttributeBehaviour<TCommand, BehaviourAttribute>)this.serviceProvider.GetRequiredService(
                serviceType: this.commandBehaviourType.MakeGenericType(typeof(TCommand), attribute.GetType())
            );
        }
    }

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

            return this.GetBehaviourAttributes(attributeCollection)
                .Select(a => 
                    new { 
                        Attribute = a, 
                        Behaviour = GetQueryAttributeBehaviour(a) 
                    }
                )
                .Aggregate(
                    seed: (QueryBehaviourDelegate<TResponse>) nextDelegate,
                    func: (n, i) => ()  => i.Behaviour.Handle(query, i.Attribute, cancellationToken, n)
                )();
        }

        private IQueryAttributeBehaviour<TQuery, TResponse, BehaviourAttribute> GetQueryAttributeBehaviour(BehaviourAttribute attribute)
        {
            return (IQueryAttributeBehaviour<TQuery, TResponse, BehaviourAttribute>)this.serviceProvider.GetRequiredService(
                serviceType: this.queryBehaviourType.MakeGenericType(typeof(TQuery), typeof(TResponse), attribute.GetType())
            );
        }
    }
}