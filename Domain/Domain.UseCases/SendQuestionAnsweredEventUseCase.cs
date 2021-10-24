using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using System.Threading;
using Common.CQRS.Abstractions.Attributes;
using Domain.Abstractions;
using Common.IAM.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions.Commands;
using Common.DDD.Abstractions.Queries;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class SendQuestionAnsweredEventUseCaseHandler : ICommandHandler<SendQuestionAnsweredEventUseCase>
    {
        private readonly IMediator mediator;

        public SendQuestionAnsweredEventUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("SEND_QUESTION_ANSWERED")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(SendQuestionAnsweredEventUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot>(command.QuestionId), cancellationToken);

            aggregateRoot.SendQuestionAnsweredEvent();

            await mediator.Send(SaveAggregateRootFactory.Create(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}