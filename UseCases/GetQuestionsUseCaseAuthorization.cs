using System.Threading.Tasks;
using Domain.Abstractions.Ports.Input;
using Synion.CQRS.Abstractions.Queries;

namespace UseCases
{
    public class GetQuestionsUseCaseAuthorization : IQueryAuthorization<GetQuestionsUseCase, GetQuestionsUseCase.Response>
    {
        public Task<bool> IsAuthorized(string identity, GetQuestionsUseCase query, GetQuestionsUseCase.Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}