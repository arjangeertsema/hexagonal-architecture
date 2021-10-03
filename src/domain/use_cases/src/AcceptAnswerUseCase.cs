using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Core;
using Reference.Domain.UseCases.Attributes;

namespace Reference.Domain.UseCases
{
    public class AcceptAnswerUseCase : IInputPortHandler<Abstractions.Ports.Input.AcceptAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore aggregateRootStore;

        public AcceptAnswerUseCase(
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
        public async Task Handle(Abstractions.Ports.Input.AcceptAnswerUseCase command)
        {
            var aggregateRoot = await aggregateRootStore.Get<AnswerQuestionsAggregateRoot>(command.QuestionId);

            var identity = await mediator.Send(new GetIdentityPort());

            aggregateRoot.AcceptAnswer
            (
                taskId: command.UserTaskId, 
                acceptedBy: identity.Id
            );

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot
            );
        }
    }
}