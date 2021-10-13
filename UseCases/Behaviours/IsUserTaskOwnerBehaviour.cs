using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Ports.Output;
using UseCases.Attributes;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Queries;

namespace UseCases.Behaviours
{
    public abstract class IsUserTaskOwnerBehaviour
    {
        private readonly IMediator mediator;

        public IsUserTaskOwnerBehaviour(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task<bool> IsUserTaskOwner(IUserTask userTask, CancellationToken cancellationToken)
        {
            if (userTask is null)
            {
                throw new ArgumentNullException(nameof(userTask));
            }

            return await mediator.Ask(new IsUserTaskOwnerPort(userTaskId: userTask.UserTaskId), cancellationToken);
        }
    }

    public class IsUserTaskOwnerCommandBehaviour<TCommand> : IsUserTaskOwnerBehaviour, ICommandAttributeBehaviour<TCommand, IsUserTaskOwnerAttribute>
        where TCommand : ICommand
    {
        public IsUserTaskOwnerCommandBehaviour(IMediator mediator) : base(mediator) { }

        public async Task Handle(TCommand command, IsUserTaskOwnerAttribute attribute, CancellationToken cancellationToken, CommandBehaviourDelegate next)
        {
            var userTask = command as IUserTask;

            if(!await IsUserTaskOwner(userTask, cancellationToken))
                throw new UnauthorizedAccessException($"User is not the owner of user task with id '{userTask.UserTaskId}'.");

            await next();
        }
    }

    public class IsUserTaskOwnerQyeryBehaviour<TQuery, TResponse> : IsUserTaskOwnerBehaviour, IQueryAttributeBehaviour<TQuery, TResponse, IsUserTaskOwnerAttribute>
        where TQuery : IQuery<TResponse>
    {
        public IsUserTaskOwnerQyeryBehaviour(IMediator mediator) : base(mediator) { }

        public async Task<TResponse> Handle(TQuery query, IsUserTaskOwnerAttribute attribute, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next)
        {            
            var userTask = query as IUserTask;

            if(!await IsUserTaskOwner(userTask, cancellationToken))
                throw new UnauthorizedAccessException($"User is not the owner of user task with id '{userTask.UserTaskId}'.");

            return await next();
        }
    }
}