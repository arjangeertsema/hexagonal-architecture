using System.Threading.Tasks;
using Domain.Abstractions.Ports.Input;
using Common.IAM.Abstractions.Queries;
using System;

namespace UseCases
{
    public class GetQuestionsUseCaseAuthorization : IQueryAuthorization<GetQuestionsUseCase, GetQuestionsUseCase.Response>
    {
        public Task Authorize(string userId, GetQuestionsUseCase query, GetQuestionsUseCase.Response response)
        {
            throw new UnauthorizedAccessException("Not implemented....");
        }
    }
}