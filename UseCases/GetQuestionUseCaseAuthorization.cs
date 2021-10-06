using System.Threading.Tasks;
using Domain.Abstractions.Ports.Input;
using Synion.CQRS.Abstractions.Queries;

namespace UseCases
{
    public class GetQuestionUseCaseAuthorization : IQueryAuthorization<GetQuestionUseCase, GetQuestionUseCase.Response>
    {
        public Task<bool> IsAuthorized(string identity, GetQuestionUseCase query, GetQuestionUseCase.Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}