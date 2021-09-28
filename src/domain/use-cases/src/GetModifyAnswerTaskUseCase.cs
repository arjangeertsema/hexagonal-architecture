using System;
using System.Threading.Tasks;
using example.domain.abstractions.attributes;
using example.domain.abstractions.ports.input;
using example.domain.abstractions;
using static example.domain.abstractions.ports.input.IGetModifyAnswerTaskUseCase;

namespace example.domain.use_cases
{
    public class GetModifyAnswerTaskUseCase : IGetModifyAnswerTaskUseCase
    {
        private readonly IPermission permission;

        public GetModifyAnswerTaskUseCase(
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
        public Task<Response> Execute(IGetModifyAnswerTaskUseCase.Query query)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermission()
        {
            return permission.HasPermission("A permission");
        }
    }
}
