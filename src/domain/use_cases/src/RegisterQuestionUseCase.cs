using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Abstractions.Ports.Output.Exceptions;
using System.Transactions;
using Reference.Domain.Core;

namespace Reference.Domain.UseCases
{
    public class RegisterQuestionUseCase : IRegisterQuestionUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;
        private readonly IRegisterCommandPort registerCommandPort;
        private readonly ISaveAggregateRootPort saveAggregateRootPort;

        public RegisterQuestionUseCase(
            IHasPermissonPort hasPermissionPort,
            IRegisterCommandPort registerCommandPort,
            ISaveAggregateRootPort saveAggregateRootPort
        )
        {
            if (hasPermissionPort is null)
            {
                throw new ArgumentNullException(nameof(hasPermissionPort));
            }

            if (registerCommandPort is null)
            {
                throw new ArgumentNullException(nameof(registerCommandPort));
            }

            if (saveAggregateRootPort is null)
            {
                throw new ArgumentNullException(nameof(saveAggregateRootPort));
            }

            this.hasPermissionPort = hasPermissionPort;
            this.registerCommandPort = registerCommandPort;
            this.saveAggregateRootPort = saveAggregateRootPort;
        }

        public async Task Execute(IRegisterQuestionUseCase.Command command)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    await CheckPermission();

                    await registerCommandPort.Execute(command);

                    var process = AnswerQuestionsProcess.Start
                    (
                        subject: command.Subject,
                        question: command.Question,
                        sender: command.Sender
                    );

                    await saveAggregateRootPort.Execute(Map(process));

                    scope.Complete();
                }
            }
            catch (CommandAlreadyExistsException)
            {
                return;
            }
        }

        private async Task CheckPermission()
        {
            var hasPermission = await hasPermissionPort.Execute(new IHasPermissonPort.Query("a permission"));
            if (!hasPermission)
            {
                throw new UnauthorizedAccessException();
            }
        }

        private ISaveAggregateRootPort.Command Map(AnswerQuestionsProcess process)
        {
            throw new NotImplementedException();
        }
    }
}
