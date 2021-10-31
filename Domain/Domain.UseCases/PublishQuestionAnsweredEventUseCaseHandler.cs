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
using Domain.Abstractions.Ports;
using Domain.Abstractions.Events;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class PublishQuestionAnsweredEventUseCaseHandler : ICommandHandler<PublishQuestionAnsweredEventUseCase>
    {
        private readonly IMediator mediator;

        public PublishQuestionAnsweredEventUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("PUBLISH_QUESTION_ANSWERED_EVENT")]
        [Transactional]
        [MakeIdempotent]
        public async Task Handle(PublishQuestionAnsweredEventUseCase command, CancellationToken cancellationToken)
        {
            var aggregateRoot = await mediator.Ask(new GetAggregateRoot<IAnswerQuestionsAggregateRoot>(command.QuestionId), cancellationToken);

            aggregateRoot.SendQuestionAnsweredEvent();

            await mediator.Send(new PublishEventPort<QuestionAnswerdIntegrationEvent>(command.CommandId, new QuestionAnswerdIntegrationEvent(command.QuestionId)));
            await mediator.Send(SaveAggregateRootFactory.Create(command.CommandId, aggregateRoot), cancellationToken);
        }
    }
}