using System.Threading.Tasks;
using Domain.Abstractions.Ports.Input;
using System;
using Common.IAM.Abstractions;

namespace UseCases
{
    public class GetQuestionUseCaseAuthorization : IQueryAuthorization<GetQuestionUseCase, GetQuestionUseCase.Response>
    {
        public Task Authorize(string userId, GetQuestionUseCase query, GetQuestionUseCase.Response response)
        {
            throw new UnauthorizedAccessException("Not implemented....");
        }
    }
}