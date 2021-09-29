using System;
using System.Threading;
using System.Threading.Tasks;
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
        IHandleDomainEventPort<QuestionRecievedEvent>, 
        IHandleDomainEventPort<QuestionAnsweredEvent>, 
        IHandleDomainEventPort<AnswerRejectedEvent>, 
        IHandleDomainEventPort<AnswerAcceptedEvent>, 
        IHandleDomainEventPort<AnswerModifiedEvent>,
        IAsyncJobHandler<SendAnswerJob>,
        IAsyncJobHandler<SendQuestionAnsweredEventJob>
    {
        private readonly IZeebeClient zeebeClient;
        private readonly IZeebeVariablesSerializer serializer;
        private readonly IZeebeVariablesDeserializer deserializer;
        private readonly ISendAnswerUseCase sendAnswerUseCase;
        private readonly ISendQuestionAnsweredEventUseCase sendQuestionAnsweredEventUseCase;

        public AnswerQuestionsService(
            IZeebeClient zeebeClient,
            IZeebeVariablesSerializer serializer,
            IZeebeVariablesDeserializer deserializer,
            ISendAnswerUseCase sendAnswerUseCase,
            ISendQuestionAnsweredEventUseCase sendQuestionAnsweredEventUseCase)
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

            if (sendAnswerUseCase is null)
            {
                throw new ArgumentNullException(nameof(sendAnswerUseCase));
            }

            if (sendQuestionAnsweredEventUseCase is null)
            {
                throw new ArgumentNullException(nameof(sendQuestionAnsweredEventUseCase));
            }

            this.zeebeClient = zeebeClient;
            this.serializer = serializer;
            this.deserializer = deserializer;
            this.sendAnswerUseCase = sendAnswerUseCase;
            this.sendQuestionAnsweredEventUseCase = sendQuestionAnsweredEventUseCase;
        }

        public async Task Execute(IHandleDomainEventPort<QuestionRecievedEvent>.Command command)
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

            // await zeebeClient.NewCreateProcessInstanceCommand()
            //     .BpmnProcessId("Process_AnswerQuestions")
            //     .Version(1)
            //     .Variables(serializer.Serialize(variables))
            //     .Send();
        }

        public async Task Execute(IHandleDomainEventPort<QuestionAnsweredEvent>.Command command)
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

        public async Task Execute(IHandleDomainEventPort<AnswerRejectedEvent>.Command command)
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

        public async Task Execute(IHandleDomainEventPort<AnswerAcceptedEvent>.Command command)
        {
            var variables = new {
                command.Event.Accepted,
                command.Event.AcceptedBy
            };

            await this.zeebeClient.NewCompleteJobCommand(command.Event.TaskId)
                .Variables(serializer.Serialize(variables))
                .SendWithRetry();
        }

        public async Task Execute(IHandleDomainEventPort<AnswerModifiedEvent>.Command command)
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
            //TODO: set identity for permission check
            var variables = deserializer.Deserialize<AnswerQuestionsVariables>(job.Variables);
            var command = new ISendAnswerUseCase.Command(variables.SendAnswerCommandId);
            await this.sendAnswerUseCase.Execute(command);
        }

        public async Task HandleJob(SendQuestionAnsweredEventJob job, CancellationToken cancellationToken)
        {
            //TODO: set identity for permission check
            var variables = deserializer.Deserialize<AnswerQuestionsVariables>(job.Variables);
            var command = new ISendQuestionAnsweredEventUseCase.Command(variables.SendQuestionAnsweredEventCommandId);
            await sendQuestionAnsweredEventUseCase.Execute(command);
        }
    }

    [FetchVariables("SendAnswerCommandId")]
    public class SendAnswerJob : AbstractJob
    {
        public SendAnswerJob(IJob job) : base(job) { }
    }

    [FetchVariables("SendQuestionAnsweredEventCommandId")]
    public class SendQuestionAnsweredEventJob : AbstractJob
    {
        public SendQuestionAnsweredEventJob(IJob job) : base(job) { }
    }

    public class AnswerQuestionsVariables
    {
        public Guid SendAnswerCommandId { get; set; }
        public Guid SendQuestionAnsweredEventCommandId { get; set; }
    }
}