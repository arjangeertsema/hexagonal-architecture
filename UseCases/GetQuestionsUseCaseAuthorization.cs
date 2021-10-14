using System.Threading.Tasks;
using Domain.Abstractions.UseCases;
using System;
using Common.IAM.Abstractions;

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