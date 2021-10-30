using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Common.IAM.Abstractions.Queries;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions.Queries;
using Common.DDD.Abstractions.Commands;
using Domain.Abstractions.Ports;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class AcceptAnswerUseCaseHandler : ICommandHandler<AcceptAnswerUseCase>
    {
        private readonly IMediator mediator;

        public AcceptAnswerUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("REVIEW_ANSWER")]
        [IsUserTaskOwner]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(AcceptAnswerUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot>(command.QuestionId), cancellationToken);

            var userId = await mediator.Ask(new GetUserId(), cancellationToken);

            aggregateRoot.AcceptAnswer
            (
                userTaskId: command.UserTaskId, 
                acceptedBy: userId
            );

            await mediator.Send(new CompleteUserTaskPort(command.CommandId, command.UserTaskId), cancellationToken);
            await mediator.Send(SaveAggregateRootFactory.Create(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}