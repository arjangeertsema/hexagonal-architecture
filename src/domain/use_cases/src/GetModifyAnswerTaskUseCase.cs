using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Domain.Abstractions.Ports.Output;

namespace Reference.Domain.UseCases
{
    public class GetModifyAnswerTaskUseCase : IGetModifyAnswerTaskUseCase
    {
        private readonly IHasPermissonPort hasPermissionPort;

        public GetModifyAnswerTaskUseCase(
            IHasPermissonPort hasPermissionPort
        )
        {
            if (hasPermissionPort is null)
            {
                throw new ArgumentNullException(nameof(hasPermissionPort));
            }

            this.hasPermissionPort = hasPermissionPort;
        }

        public async Task<IGetModifyAnswerTaskUseCase.Response> Execute(IGetModifyAnswerTaskUseCase.Query query)
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