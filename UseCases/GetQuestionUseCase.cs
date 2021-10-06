using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;
using UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;
using System.Threading;

namespace UseCases
{
    public class GetQuestionUseCaseHandler : IInputPortHandler<GetQuestionUseCase, GetQuestionUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetQuestionUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsAuthorized]
        public Task<GetQuestionUseCase.Response> Handle(GetQuestionUseCase query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}