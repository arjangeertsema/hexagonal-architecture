using System;
using System.Threading.Tasks;
using Synion.CQRS.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.UseCases.Attributes;
using Synion.CQRS.Abstractions.Ports;

namespace Reference.UseCases
{
    public class GetQuestionsUseCaseHandler : IInputPort<GetQuestionsUseCase>
    {
        private readonly IMediator mediator;

        public GetQuestionsUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetQuestionsUseCase.Response> Handle(GetQuestionsUseCase query)
        {
            throw new NotImplementedException();
        }
    }
}