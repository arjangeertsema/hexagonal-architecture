using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Events;
using Domain.Abstractions.Ports.Input;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Bootstrap.Abstractions;
using Zeebe.Client.Bootstrap.Attributes;
using Synion.DDD.Abstractions;

namespace Adapters.Zeebe
{
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
        private readonly IZeebeVariablesDeserializer deserializer;
        private readonly IMediator mediator;

        public AnswerQuestionsService(
            IZeebeClient zeebeClient,
            IZeebeVariablesSerializer serializer,
            IZeebeVariablesDeserializer deserializer,
            IMediator mediator)
        {
            this.zeebeClient = zeebeClient ?? throw new ArgumentNullException(nameof(zeebeClient));;
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));;
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));;
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));;
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
                .Send();
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

            await this.zeebeClient.NewCompleteJobCommand(@event.UserTaskId)
                .Variables(this.serializer.Serialize(variables))
                .SendWithRetry();
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

            await this.zeebeClient.NewCompleteJobCommand(@event.UserTaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
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

            await this.zeebeClient.NewCompleteJobCommand(@event.UserTaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
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

            await this.zeebeClient.NewCompleteJobCommand(@event.UserTaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task HandleJob(SendAnswerJobV1 job, CancellationToken cancellationToken)
        {
            //TODO: set system IPrincipal with IIdentiy on thread for permission check

            var variables = deserializer.Deserialize<AnswerQuestionsVariables>(job.Variables);
            var command = new SendAnswerUseCase
            (
                commandId: variables.SendAnswerCommandId,
                questionId: variables.QuestionId
            );

            await this.mediator.Send(command, cancellationToken);
        }

        public async Task HandleJob(SendQuestionAnsweredEventJobV1 job, CancellationToken cancellationToken)
        {
            //TODO: set system IPrincipal with IIdentiy on thread for permission check

            var variables = deserializer.Deserialize<AnswerQuestionsVariables>(job.Variables);
            var command = new SendQuestionAnsweredEventUseCase
            (
                commandId: variables.SendQuestionAnsweredEventCommandId,
                questionId: variables.QuestionId
            );

            await mediator.Send(command, cancellationToken);
        }
    }

    [FetchVariables("QuestionId", "SendAnswerCommandId")]
    public class SendAnswerJobV1 : AbstractJob
    {
        public SendAnswerJobV1(IJob job) : base(job) { }
    }

    [FetchVariables("QuestionId", "SendQuestionAnsweredEventCommandId")]
    public class SendQuestionAnsweredEventJobV1 : AbstractJob
    {
        public SendQuestionAnsweredEventJobV1(IJob job) : base(job) { }
    }

    public class AnswerQuestionsVariables
    {
        public Guid QuestionId { get; set; }
        public Guid SendAnswerCommandId { get; set; }
        public Guid SendQuestionAnsweredEventCommandId { get; set; }
    }
}