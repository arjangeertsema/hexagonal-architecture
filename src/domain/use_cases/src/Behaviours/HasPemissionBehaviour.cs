using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.UseCases.Attributes;

namespace Reference.Domain.UseCases.Behaviours
{
    public class HasPemissionBehaviour : ICommandBehaviour, IQueryBehaviour
    {
        private readonly IMediator mediator;

        public HasPemissionBehaviour(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }
        
        public async Task Handle(ICommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandHandlerDelegate next)
        {
            var attr = attributeCollection.GetAttribute<HasPermissionAttribute>();
            if(attr == null)
            {
                await next();
                return;
            }

            if(! await mediator.Send(new HasPermissionPort(attr.Permissions.ToArray())))
                throw new UnauthorizedAccessException();

            await next();
        }

        public async Task<object> Handle(IQuery<object> query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryHandlerDelegate<object> next)
        {
            var attr = attributeCollection.GetAttribute<HasPermissionAttribute>();
            if(attr == null)
            {
                return await next();                
            }

            if(! await mediator.Send(new HasPermissionPort(attr.Permissions.ToArray())))
                throw new UnauthorizedAccessException();

            return await next();
        }
    }
}