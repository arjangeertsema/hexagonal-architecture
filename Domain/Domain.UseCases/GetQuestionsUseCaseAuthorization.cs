using System.Threading.Tasks;
using Domain.Abstractions.UseCases;
using System;
using Common.IAM.Abstractions;
using Common.CQRS.Abstractions.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.UseCases
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public class GetQuestionsUseCaseAuthorization : IQueryAuthorization<GetQuestionsUseCase, GetQuestionsUseCase.Response>
    {
        public Task Authorize(string userId, GetQuestionsUseCase query, GetQuestionsUseCase.Response response)
        {
            throw new UnauthorizedAccessException("Not implemented....");
        }
    }
}