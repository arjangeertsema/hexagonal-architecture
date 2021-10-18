using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Common.DDD.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class SendAnswerUseCaseHandler : ICommandHandler<SendAnswerUseCase>
    {
        private readonly IMediator mediator;
        private readonly IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore;

        public SendAnswerUseCaseHandler(IMediator mediator, IAggregateRootStore<IAnswerQuestionsAggregateRoot> aggregateRootStore)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.aggregateRootStore = aggregateRootStore ?? throw new ArgumentNullException(nameof(aggregateRootStore));         
         }

        
        [HasPermission("SEND_ANSWER")]
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