using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using UseCases.Attributes;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Queries;
using Common.IAM.Abstractions.Queries;

namespace UseCases.Behaviours
{
    public class HasPemissionCommandBehaviour<TCommand> : ICommandAttributeBehaviour<TCommand, HasPermissionAttribute>
        where TCommand : ICommand
    {
        private readonly IMediator mediator;

        public HasPemissionCommandBehaviour(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Handle(TCommand command, HasPermissionAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            if(! await mediator.Ask(new HasPermission(attribute.Permissions.ToArray()), cancellationToken))
                throw new UnauthorizedAccessException();

            await next();
        }
    }

    public class HasPemissionQueryBehaviour<TQuery, TResponse> : IQueryAttributeBehaviour<TQuery, TResponse, HasPermissionAttribute>
        where TQuery : IQuery<TResponse>
    {
        private readonly IMediator mediator;

        public HasPemissionQueryBehaviour(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task<TResponse> Handle(TQuery query, HasPermissionAttribute attribute, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next)
        {
            if(! await mediator.Ask(new HasPermission(attribute.Permissions.ToArray()), cancellationToken))
                throw new UnauthorizedAccessException();

            return await next();
        }
    }
}