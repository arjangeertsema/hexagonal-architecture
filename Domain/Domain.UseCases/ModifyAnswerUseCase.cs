using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Queries;
using Common.IAM.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions.Commands;
using Common.DDD.Abstractions.Queries;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class ModifyAnswerUseCaseHandler : ICommandHandler<ModifyAnswerUseCase>
    {
        private readonly IMediator mediator;

        public ModifyAnswerUseCaseHandler(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HasPermission("ANSWER_QUESTION")]
        [IsUserTaskOwner]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(ModifyAnswerUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot>(command.QuestionId), cancellationToken);

            var userId = await mediator.Ask(new GetUserId(), cancellationToken);

            aggregateRoot.ModifyAnswer
            (
                userTaskId: command.UserTaskId, 
                answer: command.Answer, 
                modifiedBy: userId
            );

            await mediator.Send(new SaveAggregateRoot<IAnswerQuestionsAggregateRoot>(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}