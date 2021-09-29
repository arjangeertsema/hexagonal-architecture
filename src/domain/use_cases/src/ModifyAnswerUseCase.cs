using System;
using System.Threading.Tasks;
using System.Transactions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Abstractions.Ports.Output.Exceptions;

namespace Reference.Domain.UseCases
{
    public class ModifyAnswerUseCase : IModifyAnswerUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;
        private readonly IRegisterCommandPort registerCommandPort;

        public ModifyAnswerUseCase(
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

        public async Task Execute(IModifyAnswerUseCase.Command command)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    await CheckPermission();

                    await registerCommandPort.Execute(command);

                    throw new NotImplementedException();

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