using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Core;
using Reference.Domain.UseCases.Attributes;

namespace Reference.Domain.UseCases
{
    public class RejectAnswerUseCase : IInputPortHandler<Abstractions.Ports.Input.RejectAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public RejectAnswerUseCase(
            IMediator mediator,
            IAggregateRootStore aggregateRootStore
        )
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            if (aggregateRootStore is null)
            {
                throw new ArgumentNullException(nameof(aggregateRootStore));
            }

            this.mediator = mediator;
            this.aggregateRootStore = aggregateRootStore;         
         }

        [Transactional]
        [HasPermission("a permission")]
        [IsUserTaskOwner]
        [MakeIdempotent]
        public async Task Handle(Abstractions.Ports.Input.RejectAnswerUseCase command)
        {
            var aggregateRoot = await aggregateRootStore.Get<AnswerQuestionsAggregateRoot>(command.QuestionId);

            var identity = await mediator.Send(new GetIdentityPort());

            aggregateRoot.RejectAnswer
            (
                taskId: command.UserTaskId, 
                rejection: command.Rejection, 
                rejectedBy: identity.Id
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot
            );
        }
    }
}