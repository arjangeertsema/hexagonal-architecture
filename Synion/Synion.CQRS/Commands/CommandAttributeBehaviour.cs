using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;

namespace Synion.CQRS.Commands
{
    internal class CommandAttributeBehaviour<TCommand> : AttributeBehaviour, ICommandBehaviour<TCommand>
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

            var pipeline = this.GetBehaviourAttributes(attributeCollection)
                .Select(a => 
                    new { 
                        Attribute = a, 
                        Behaviour = GetCommandAttributeBehaviour(a) 
                    }
                )
                .Aggregate(
                    seed: (CommandBehaviourDelegate) nextDelegate,
                    func: (n, i) => () => i.Behaviour.Handle(command, i.Attribute, cancellationToken, n)
                );

            return pipeline();
        }

        private ICommandAttributeBehaviour<TCommand, BehaviourAttribute> GetCommandAttributeBehaviour(BehaviourAttribute attribute)
        {
            return (ICommandAttributeBehaviour<TCommand, BehaviourAttribute>)this.serviceProvider.GetRequiredService(
                serviceType: this.commandBehaviourType.MakeGenericType(typeof(TCommand), attribute.GetType())
            );
        }
    }
}