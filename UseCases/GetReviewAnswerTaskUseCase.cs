using System;
using System.Threading.Tasks;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;
using Common.CQRS.Abstractions.Queries;
using System.Threading;
using Common.IAM.Abstractions.Attributes;
using Common.UserTasks.Abstractions.Attributes;

namespace UseCases
{
    public class GetReviewAnswerTaskUseCaseHandler : IQueryHandler<GetReviewAnswerTaskUseCase, GetReviewAnswerTaskUseCase.Response>
    {
        private readonly IMediator mediator;

        public GetReviewAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HasPermission("a permission")]
        [IsUserTaskOwner]
        public Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}