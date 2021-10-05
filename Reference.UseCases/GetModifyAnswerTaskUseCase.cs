using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;

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
