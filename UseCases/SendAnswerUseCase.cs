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
using Synion.CQRS.Abstractions.Attributes;

namespace UseCases
{
    public class SendAnswerUseCaseHandler : IInputPortHandler<SendAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<AnswerQuestionsAggregateRoot> aggregateRootStore;

        public SendAnswerUseCaseHandler(IMediator mediator, IAggregateRootStore<AnswerQuestionsAggregateRoot> aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));         
         }

        
        [HasPermission("a permission")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(SendAnswerUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await aggregateRootStore.Get(command.QuestionId, cancellationToken);

            var identity = await mediator.Ask(new GetIdentityPort(), cancellationToken);

            aggregateRoot.SendAnswer();

            await aggregateRootStore.Save
            (
                commandId: command.CommandId, 
                aggregateRoot: aggregateRoot,
                cancellationToken: cancellationToken
            );
        }
    }
}