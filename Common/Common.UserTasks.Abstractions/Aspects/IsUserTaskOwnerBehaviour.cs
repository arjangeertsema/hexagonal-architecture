using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Aspects;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Queries;
using Common.UserTasks.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Queries;

namespace Common.UserTasks.Abstractions.Aspects
{
    public abstract class IsUserTaskOwnerAspect
    {
        private readonly IMediator mediator;

        public IsUserTaskOwnerAspect(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected async Task AuthorizeUserTaskOwner(IUserTask userTask, CancellationToken cancellationToken)
        {
            if (userTask is null)
            {
                throw new ArgumentNullException(nameof(userTask));
            }

            if(!await mediator.Ask(new IsUserTaskOwner(userTaskId: userTask.UserTaskId), cancellationToken))
                throw new UnauthorizedAccessException($"User is not the owner of user task with id '{userTask.UserTaskId}'.");
        }
    }

    public class IsUserTaskOwnerCommandAspect<TCommand> : IsUserTaskOwnerAspect, ICommandAttributeAspect<TCommand, IsUserTaskOwnerAttribute>
        where TCommand : ICommand
    {
        public IsUserTaskOwnerCommandAspect(IMediator mediator) : base(mediator) { }

        public async Task Handle(TCommand command, IsUserTaskOwnerAttribute attribute, CancellationToken cancellationToken, CommandAspectDelegate next)
        {
            await AuthorizeUserTaskOwner(command as IUserTask, cancellationToken);
            await next();
        }
    }
    public class IsUserTaskOwnerQyeryAspect<TQuery, TResponse> : IsUserTaskOwnerAspect, IQueryAttributeAspect<TQuery, TResponse, IsUserTaskOwnerAttribute>
        where TQuery : IQuery<TResponse>
    {
        public IsUserTaskOwnerQyeryAspect(IMediator mediator) : base(mediator) { }

        public async Task<TResponse> Handle(TQuery query, IsUserTaskOwnerAttribute attribute, CancellationToken cancellationToken, QueryAspectDelegate<TResponse> next)
        {            
            await AuthorizeUserTaskOwner(query as IUserTask, cancellationToken);
            return await next();
        }
    }
}