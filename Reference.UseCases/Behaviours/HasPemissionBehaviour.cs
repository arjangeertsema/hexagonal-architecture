using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace Reference.UseCases.Behaviours
{
    public class HasPemissionCommandBehaviour<TCommand> : ICommandAttributeBehaviour<TCommand, HasPermissionAttribute>
        where TCommand : ICommand
    {
        private readonly IMediator mediator;

        public HasPemissionCommandBehaviour(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public async Task Handle(TCommand command, HasPermissionAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            if(! await mediator.Send(new HasPermissionPort(attribute.Permissions.ToArray())))
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
            if(! await mediator.Send(new HasPermissionPort(attribute.Permissions.ToArray())))
                throw new UnauthorizedAccessException();

            return await next();
        }
    }
}