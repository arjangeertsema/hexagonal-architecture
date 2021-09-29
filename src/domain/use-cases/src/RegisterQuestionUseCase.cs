using System;
using System.Threading.Tasks;
using example.domain.abstractions.ports.input;
using example.domain.core;
using example.domain.abstractions.ports.output;
using example.domain.abstractions.ports.output.exceptions;
using System.Transactions;

namespace example.domain.use_cases
{
    public class RegisterQuestionUseCase : IRegisterQuestionUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;
        private readonly IRegisterCommandPort registerCommandPort;

        public RegisterQuestionUseCase(
            IHasPermissonPort hasPermissionPort,
            IRegisterCommandPort registerCommandPort
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

            this.hasPermissionPort = hasPermissionPort;
            this.registerCommandPort = registerCommandPort;
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

                    //TODO: save state

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
    }
}
