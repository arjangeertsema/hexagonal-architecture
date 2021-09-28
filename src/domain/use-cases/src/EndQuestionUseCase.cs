using System;
using System.Threading.Tasks;
using example.domain.abstractions.attributes;
using example.domain.abstractions.ports.input;
using example.domain.abstractions;

namespace example.domain.use_cases
{
    public class EndQuestionUseCase : IEndQuestionUseCase
    {
        private readonly IPermission permission;

        public EndQuestionUseCase(
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
        public Task Execute(IEndQuestionUseCase.Command command)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPermission()
        {
            return permission.HasPermission("A permission");
        }
    }
}