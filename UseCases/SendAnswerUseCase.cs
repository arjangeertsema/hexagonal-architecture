using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using Common.CQRS.Abstractions.Commands;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;

namespace UseCases
{
    public class SendAnswerUseCaseHandler : ICommandHandler<SendAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore;

        public SendAnswerUseCaseHandler(IMediator mediator, IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore)
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