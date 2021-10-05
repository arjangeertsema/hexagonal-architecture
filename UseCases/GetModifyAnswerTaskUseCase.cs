using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace UseCases
{
    public class GetModifyAnswerTaskUseCaseHandler : IInputPortHandler<GetModifyAnswerTaskUseCase, GetModifyAnswerTaskUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetModifyAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetModifyAnswerTaskUseCase.Response> Handle(GetModifyAnswerTaskUseCase query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
