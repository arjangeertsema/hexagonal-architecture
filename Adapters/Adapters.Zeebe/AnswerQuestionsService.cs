using System;
using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Events;
using Domain.Abstractions.UseCases;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Bootstrap.Abstractions;
using Common.DDD.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions.Attributes;
using Common.IAM.Abstractions.Commands;

namespace Adapters.Zeebe
{
    [ServiceLifetime(ServiceLifetime.Scoped)]
    public class AnswerQuestionsService : 
        IDomainEventHandler<QuestionRecievedEvent>,
        IDomainEventHandler<QuestionAnsweredEvent>, 
        IDomainEventHandler<AnswerRejectedEvent>, 
        IDomainEventHandler<AnswerAcceptedEvent>, 
        IDomainEventHandler<AnswerModifiedEvent>,
        IAsyncJobHandler<SendAnswerJobV1>,
        IAsyncJobHandler<SendQuestionAnsweredEventJobV1>
    {
        private readonly IZeebeClient zeebeClient;
        private readonly IZeebeVariablesSerializer serializer;
        private readonly IMediator mediator;

        public AnswerQuestionsService(
            IZeebeClient zeebeClient,
            IZeebeVariablesSerializer serializer,
            IMediator mediator)
        {
            this.zeebeClient = zeebeClient ?? throw new ArgumentNullException(nameof(zeebeClient));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(QuestionRecievedEvent @event, CancellationToken cancellationToken)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var variables = new {
                @event.AggregateRootId,
                @event.Subject,
                @event.Question,
                @event.Asked,
                @event.AskedBy,
                SendAnswerCommandId = Guid.NewGuid(),
                SendQuestionAnsweredEventCommandId = Guid.NewGuid()
            };

            await zeebeClient.NewPublishMessageCommand()
                .MessageName("Message_QuestionRecieved_V1")
                .CorrelationKey(@event.AggregateRootId.ToString())
                .MessageId(@event.EventId.ToString())
                .Variables(this.serializer.Serialize(variables))
                .Send(cancellationToken);
        }

        public async Task Handle(QuestionAnsweredEvent @event, CancellationToken cancellationToken)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var variables = new {
                @event.Answer,
                @event.Answered,
                @event.AnsweredBy
            };

            await this.zeebeClient.NewCompleteJobCommand(long.Parse(@event.UserTaskId))
                .Variables(this.serializer.Serialize(variables))
                .SendWithRetry(null, cancellationToken);
        }

        public async Task Handle(AnswerRejectedEvent @event, CancellationToken cancellationToken)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var variables = new {
                @event.Rejected,
                @event.RejectedBy,
                @event.Rejection
            };

            await this.zeebeClient.NewCompleteJobCommand(long.Parse(@event.UserTaskId))
                .Variables(serializer.Serialize(variables))
                .SendWithRetry(null, cancellationToken);
        }

        public async Task Handle(AnswerAcceptedEvent @event, CancellationToken cancellationToken)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var variables = new {
                @event.Accepted,
                @event.AcceptedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(long.Parse(@event.UserTaskId))
                .Variables(serializer.Serialize(variables))
                .SendWithRetry(null, cancellationToken);
        }

        public async Task Handle(AnswerModifiedEvent @event, CancellationToken cancellationToken)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var variables = new {
                @event.Answer,
                @event.Modified,
                @event.ModifiedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(long.Parse(@event.UserTaskId))
                .Variables(serializer.Serialize(variables))
                .SendWithRetry(null, cancellationToken);
        }

        public async Task HandleJob(SendAnswerJobV1 job, CancellationToken cancellationToken)
        {
            await mediator.Send(new AuthenticateSystem(Guid.NewGuid()));

            var command = new SendAnswerUseCase
            (
                commandId: job.State.SendAnswerCommandId,
                questionId: job.State.QuestionId
            );

            await this.mediator.Send(command, cancellationToken);
        }

        public async Task HandleJob(SendQuestionAnsweredEventJobV1 job, CancellationToken cancellationToken)
        {
            await mediator.Send(new AuthenticateSystem(Guid.NewGuid()));

            var command = new SendQuestionAnsweredEventUseCase
            (
                commandId: job.State.SendQuestionAnsweredEventCommandId,
                questionId: job.State.QuestionId
            );

            await mediator.Send(command, cancellationToken);
        }
    }

    public class SendAnswerJobV1 : AbstractJob<SendAnswerJobV1.JobState>
    {
        public SendAnswerJobV1(IJob job, SendAnswerJobV1.JobState state) : base(job, state) { }

        public class JobState
        {
            public Guid QuestionId { get; set; }
            public Guid SendAnswerCommandId { get; set; }
        }
    }

    public class SendQuestionAnsweredEventJobV1 : AbstractJob<SendQuestionAnsweredEventJobV1.JobState>
    {
        public SendQuestionAnsweredEventJobV1(IJob job, SendQuestionAnsweredEventJobV1.JobState state) : base(job, state) { }

        public class JobState
        {
            public Guid QuestionId { get; set; }
            public Guid SendQuestionAnsweredEventCommandId { get; set; }
        }
    }
}