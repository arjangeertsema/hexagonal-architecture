using System;
using System.Threading;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Core.AnswerQuestions;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Bootstrap.Abstractions;
using Zeebe.Client.Bootstrap.Attributes;

namespace Reference.Adapters.Zeebe
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
            if (zeebeClient is null)
            {
                throw new ArgumentNullException(nameof(zeebeClient));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (deserializer is null)
            {
                throw new ArgumentNullException(nameof(deserializer));
            }

            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.zeebeClient = zeebeClient;
            this.serializer = serializer;
            this.deserializer = deserializer;
            this.mediator = mediator;
        }

        public async Task Handle(HandleDomainEventPort<QuestionRecievedEvent> command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var variables = new {
                command.Event.AggregateId,
                command.Event.Subject,
                command.Event.Question,
                command.Event.Asked,
                command.Event.AskedBy,
                SendAnswerCommandId = Guid.NewGuid(),
                SendQuestionAnsweredEventCommandId = Guid.NewGuid()
            };

            await zeebeClient.NewPublishMessageCommand()
                .MessageName("Message_QuestionRecieved")
                .CorrelationKey(command.CommandId.ToString())
                .MessageId(command.CommandId.ToString())
                .Send();
        }

        public async Task Handle(HandleDomainEventPort<QuestionAnsweredEvent> command)
        {
            var variables = new {
                command.Event.Answer,
                command.Event.Answered,
                command.Event.AnsweredBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.TaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Handle(HandleDomainEventPort<AnswerRejectedEvent> command)
        {
            var variables = new {
                command.Event.Rejected,
                command.Event.RejectedBy,
                command.Event.Rejection
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.TaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Handle(HandleDomainEventPort<AnswerAcceptedEvent> command)
        {
            var variables = new {
                command.Event.Accepted,
                command.Event.AcceptedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.TaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Handle(HandleDomainEventPort<AnswerModifiedEvent> command)
        {
            var variables = new {
                command.Event.Answer,
                command.Event.Modified,
                command.Event.ModifiedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.TaskId)
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