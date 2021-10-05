using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;

namespace Synion.CQRS
{
    internal class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly IServiceProvider serviceProvider;

        public CommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TCommand command, CancellationToken cancellationToken)
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            var attributes = GetHandlerAttributes(handler);
            Task handleDelegate() => handler.Handle(command, cancellationToken);

            return serviceProvider
                .GetServices<ICommandBehaviour<TCommand>>()
                .Reverse()
                .Aggregate((CommandBehaviourDelegate) handleDelegate, (next, behaviour) => () => behaviour.Handle(command, attributes, cancellationToken, next))();        
        }

        private static IAttributeCollection GetHandlerAttributes(ICommandHandler<TCommand> handler) 
        {
            var name = nameof(ICommandHandler<TCommand>.Handle);
            var reference = typeof(ICommandHandler<TCommand>)
                .GetMethods()
                .Where(m => m.Name.Equals(name))
                .sin;

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