using System;
using System.Threading;
using System.Threading.Tasks;
using example.domain.abstractions.ports.input;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Bootstrap.Abstractions;

namespace example.adapters.zeebe
{
    public class SendMessageJobHandler : IAsyncJobHandler<SendMessageJob>
    {
        private readonly ISendAnswerUseCase sendAnswerUseCase;

        public SendMessageJobHandler(ISendAnswerUseCase sendAnswerUseCase)
        {
            if (sendAnswerUseCase is null)
            {
                throw new ArgumentNullException(nameof(sendAnswerUseCase));
            }

            this.sendAnswerUseCase = sendAnswerUseCase;
        }
        public async Task HandleJob(SendMessageJob job, CancellationToken cancellationToken)
        {
            //TODO: set identity for permission
            var command = Map(job);
            await this.sendAnswerUseCase.Execute(command);
        }

        public static ISendAnswerUseCase.Command Map(SendMessageJob sendMessageJob)
        {
            throw new NotImplementedException();
        }
    }

    public class SendMessageJob : AbstractJob
    {
        public SendMessageJob(IJob job) 
        : base(job)
        { }
    }
}