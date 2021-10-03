using System;
using System.Threading;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Abstractions.Ports.Output.Exceptions;
using Reference.UseCases.Attributes;

namespace Reference.UseCases.Behaviours
{
    public class MakeIdempotentBehaviour : ICommandBehaviour
    {
        private readonly IMediator mediator;

        public MakeIdempotentBehaviour(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        public async Task Handle(ICommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next)
        {
            var attr = attributeCollection.GetAttribute<MakeIdempotentAttribute>();
            if(attr == null)
            {
                await next();
                return;
            }

            try
            {
                await this.mediator.Send(new RegisterCommandPort(command));
                await next();
            }
            catch(CommandAlreadyExistsException)
            {
                //Stop pipeline command has already been handled.
                return;
            }
        }
    }
}