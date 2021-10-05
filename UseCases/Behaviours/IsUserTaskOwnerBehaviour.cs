using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Ports.Output;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Queries;

namespace UseCases.Behaviours
{
    public abstract class IsUserTaskOwnerBehaviour
    {
        private readonly IMediator mediator;

        public IsUserTaskOwnerBehaviour(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<bool> IsUserTaskOwner(IUserTask userTask)
        {
            if (userTask is null)
            {
                throw new ArgumentNullException(nameof(userTask));
            }

            var identity = await mediator.Send(new GetIdentityPort());
            return await mediator.Send(new IsUserTaskOwnerPort(identity: identity.Id, userTaskId: userTask.UserTaskId));
        }
    }

    public class IsUserTaskOwnerCommandBehaviour<TCommand> : IsUserTaskOwnerBehaviour, ICommandAttributeBehaviour<TCommand, IsUserTaskOwnerAttribute>
        where TCommand : ICommand
    {
        public IsUserTaskOwnerCommandBehaviour(IMediator mediator) : base(mediator) { }

        public async Task Handle(TCommand command, IsUserTaskOwnerAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            if(!await IsUserTaskOwner(command as IUserTask))
                throw new UnauthorizedAccessException();

            await next();
        }
    }

    public class IsUserTaskOwnerQyeryBehaviour<TQuery, TResponse> : IsUserTaskOwnerBehaviour, IQueryAttributeBehaviour<TQuery, TResponse, IsUserTaskOwnerAttribute>
        where TQuery : IQuery<TResponse>
    {
        public IsUserTaskOwnerQyeryBehaviour(IMediator mediator) : base(mediator) { }

        public async Task<TResponse> Handle(TQuery query, IsUserTaskOwnerAttribute attribute, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next)
        {            
            if(!await IsUserTaskOwner(query as IUserTask))
                throw new UnauthorizedAccessException();

            return await next();
        }
    }
}