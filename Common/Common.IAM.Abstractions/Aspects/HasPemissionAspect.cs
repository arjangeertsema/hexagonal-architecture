using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Aspects;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Queries;
using Common.IAM.Abstractions.Attributes;
using Common.IAM.Abstractions.Queries;

namespace Common.IAM.Abstractions.Aspects
{
    public class HasPemissionCommandAspect<TCommand> : ICommandAttributeAspect<TCommand, HasPermissionAttribute>
        where TCommand : ICommand
    {
        private readonly IMediator mediator;

        public HasPemissionCommandAspect(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Handle(TCommand command, HasPermissionAttribute attribute, CancellationToken cancellationToken, CommandAspectDelegate next)
        {
            if(! await mediator.Ask(new HasPermission(attribute.Permissions.ToArray()), cancellationToken))
                throw new UnauthorizedAccessException();

            await next();
        }
    }

    public class HasPemissionQueryAspect<TQuery, TResponse> : IQueryAttributeAspect<TQuery, TResponse, HasPermissionAttribute>
        where TQuery : IQuery<TResponse>
    {
        private readonly IMediator mediator;

        public HasPemissionQueryAspect(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task<TResponse> Handle(TQuery query, HasPermissionAttribute attribute, CancellationToken cancellationToken, QueryAspectDelegate<TResponse> next)
        {
            if(! await mediator.Ask(new HasPermission(attribute.Permissions.ToArray()), cancellationToken))
                throw new UnauthorizedAccessException();

            return await next();
        }
    }
    
}