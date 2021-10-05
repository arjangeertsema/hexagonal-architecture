using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Synion.DDD.Abstractions;
using Domain.Abstractions.Ports.Input;
using Domain.Abstractions.Ports.Output;
using Domain.Core;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace UseCases
{
    public class ModifyAnswerUseCaseHandler : IInputPortHandler<ModifyAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public ModifyAnswerUseCaseHandler(IMediator mediator, IAggregateRootStore aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));           
        }

        [Transactional]
        [HasPermission("a permission")]
        [IsUserTaskOwner]
        [MakeIdempotent]
        public async Task Handle(ModifyAnswerUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await aggregateRootStore.Get<AnswerQuestionsAggregateRoot>(command.QuestionId);

            var identity = await mediator.Send(new GetIdentityPort());

            aggregateRoot.ModifyAnswer
            (
                taskId: command.UserTaskId, 
                answer: command.Answer, 
                modifiedBy: identity.Id
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot
            );
        }
    }
}