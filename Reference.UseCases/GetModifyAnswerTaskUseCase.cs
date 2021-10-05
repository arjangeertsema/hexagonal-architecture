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

        public GetModifyAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetModifyAnswerTaskUseCase.Response> Handle(GetModifyAnswerTaskUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}
