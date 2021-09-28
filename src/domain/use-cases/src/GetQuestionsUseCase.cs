using System;
using System.Threading.Tasks;
using example.domain.abstractions.attributes;
using example.domain.abstractions.ports.input;
using example.domain.abstractions;
using static example.domain.abstractions.ports.input.IGetQuestionsUseCase;

namespace example.domain.use_cases
{
    public class GetQuestionsUseCase : IGetQuestionsUseCase
    {
        private readonly IPermission permission;

        public GetQuestionsUseCase(
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
        public Task<Response> Execute(IGetQuestionsUseCase.Query query)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermission()
        {
            return permission.HasPermission("A permission");
        }
    }
}
