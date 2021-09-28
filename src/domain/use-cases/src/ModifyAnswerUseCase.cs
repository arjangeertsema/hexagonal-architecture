using System;
using System.Threading.Tasks;
using example.domain.abstractions.attributes;
using example.domain.abstractions.ports.input;
using example.domain.abstractions;

namespace example.domain.use_cases
{
    public class ModifyAnswerUseCase : IModifyAnswerUseCase
    {
        private readonly IPermission permission;

        public ModifyAnswerUseCase(
            IPermission permission
        )
        {
            if (permission is null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            this.permission = permission;
        }

        [PreAuthorize(nameof(HasPermission))]
        public Task Execute(IModifyAnswerUseCase.Command command)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPermission()
        {
            return permission.HasPermission("A permission");
        }
    }
}