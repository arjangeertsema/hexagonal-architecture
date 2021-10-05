using System;
using System.Threading;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Events;
using Domain.Abstractions.Ports.Input;
using Domain.Abstractions.Ports.Output;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Bootstrap.Abstractions;
using Zeebe.Client.Bootstrap.Attributes;
using Synion.CQRS.Abstractions.Ports;

namespace Adapters.Zeebe
{
    public class AnswerQuestionsService : 
        IOutputPortHandler<HandleDomainEventPort<QuestionRecievedEvent>>,
        IOutputPortHandler<HandleDomainEventPort<QuestionAnsweredEvent>>, 
        IOutputPortHandler<HandleDomainEventPort<AnswerRejectedEvent>>, 
        IOutputPortHandler<HandleDomainEventPort<AnswerAcceptedEvent>>, 
        IOutputPortHandler<HandleDomainEventPort<AnswerModifiedEvent>>,
        IAsyncJobHandler<SendAnswerJob>,
        IAsyncJobHandler<SendQuestionAnsweredEventJob>
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

        public async Task Handle(HandleDomainEventPort<QuestionRecievedEvent> command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var variables = new {
                command.Event.AggregateRootId,
                command.Event.Subject,
                command.Event.Question,
                command.Event.Asked,
                command.Event.AskedBy,
                SendAnswerCommandId = Guid.NewGuid(),
                SendQuestionAnsweredEventCommandId = Guid.NewGuid()
            };

            await zeebeClient.NewPublishMessageCommand()
                .MessageName("Message_QuestionRecieved")
                .CorrelationKey(command.Event.AggregateRootId.ToString())
                .MessageId(command.CommandId.ToString())
                .Variables(this.serializer.Serialize(variables))
                .Send();
        }

        public async Task Handle(HandleDomainEventPort<QuestionAnsweredEvent> command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var variables = new {
                command.Event.Answer,
                command.Event.Answered,
                command.Event.AnsweredBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.UserTaskId)
                .Variables(this.serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Handle(HandleDomainEventPort<AnswerRejectedEvent> command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var variables = new {
                command.Event.Rejected,
                command.Event.RejectedBy,
                command.Event.Rejection
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.UserTaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Handle(HandleDomainEventPort<AnswerAcceptedEvent> command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var variables = new {
                command.Event.Accepted,
                command.Event.AcceptedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.UserTaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Handle(HandleDomainEventPort<AnswerModifiedEvent> command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var variables = new {
                command.Event.Answer,
                command.Event.Modified,
                command.Event.ModifiedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.UserTaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task HandleJob(SendAnswerJob job, CancellationToken cancellationToken)
        {
            //TODO: set system IPrincipal with IIdentiy on thread for permission check

            var variables = deserializer.Deserialize<AnswerQuestionsVariables>(job.Variables);
            var command = new SendAnswerUseCase
            (
                commandId: variables.SendAnswerCommandId,
                questionId: variables.QuestionId
            );

            await this.mediator.Send(command);
        }

        public async Task HandleJob(SendQuestionAnsweredEventJob job, CancellationToken cancellationToken)
        {
            //TODO: set system IPrincipal with IIdentiy on thread for permission check

            var variables = deserializer.Deserialize<AnswerQuestionsVariables>(job.Variables);
            var command = new SendQuestionAnsweredEventUseCase
            (
                commandId: variables.SendQuestionAnsweredEventCommandId,
                questionId: variables.QuestionId
            );

            await mediator.Send(command);
        }
    }

    [FetchVariables("QuestionId", "SendAnswerCommandId")]
    public class SendAnswerJob : AbstractJob
    {
        public SendAnswerJob(IJob job) : base(job) { }
    }

    [FetchVariables("QuestionId", "SendQuestionAnsweredEventCommandId")]
    public class SendQuestionAnsweredEventJob : AbstractJob
    {
        public SendQuestionAnsweredEventJob(IJob job) : base(job) { }
    }

    public class AnswerQuestionsVariables
    {
        public Guid QuestionId { get; set; }
        public Guid SendAnswerCommandId { get; set; }
        public Guid SendQuestionAnsweredEventCommandId { get; set; }
    }
}