using System;
using System.Threading.Tasks;
using example.domain.abstractions.ports.input;
using example.domain.abstractions.ports.output;

namespace example.domain.use_cases
{
    public class GetQuestionsUseCase : IGetQuestionsUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;

        public GetQuestionsUseCase(
            IHasPermissonPort hasPermissionPort
        )
        {
            if (hasPermissionPort is null)
            {
                throw new ArgumentNullException(nameof(hasPermissionPort));
            }

            this.hasPermissionPort = hasPermissionPort;
        }

        public async Task<IGetQuestionsUseCase.Response> Execute(IGetQuestionsUseCase.Query query)
        {
            await CheckPermission();

            throw new NotImplementedException();
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
