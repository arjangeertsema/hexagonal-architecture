using System;
using System.Threading.Tasks;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;

namespace Reference.UseCases
{
    public class GetModifyAnswerTaskUseCaseHandler : IInputPort<GetModifyAnswerTaskUseCase>
    {
        private readonly IMediator mediator;

        public GetModifyAnswerTaskUseCaseHandler(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public async Task<GetModifyAnswerTaskUseCase.Response> Handle(GetModifyAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}
