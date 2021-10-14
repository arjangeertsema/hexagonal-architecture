using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Aspects;

namespace Common.CQRS.Commands
{
    internal class AspectCommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly IServiceProvider serviceProvider;

        public AspectCommandHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task Handle(TCommand command, CancellationToken cancellationToken)
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            var attributes = GetHandlerAttributes(handler);
            Task handleDelegate() => handler.Handle(command, cancellationToken);

            var aspects = serviceProvider
                .GetServices<ICommandAspect<TCommand>>()
                .Reverse()
                .Aggregate(
                    seed: (CommandAspectDelegate) handleDelegate, 
                    func: (next, behaviour) => () => behaviour.Handle(command, attributes, cancellationToken, next)
                );

            return aspects();
        }

        private static IAttributeCollection GetHandlerAttributes(ICommandHandler<TCommand> handler) 
        {
            var name = nameof(ICommandHandler<TCommand>.Handle);
            var reference = typeof(ICommandHandler<TCommand>)
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