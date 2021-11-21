namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetQuestionsUseCaseAuthorization : IQueryAuthorization<GetQuestionsUseCase, GetQuestionsUseCase.Response>
{
    public Task Authorize(string userId, GetQuestionsUseCase query, GetQuestionsUseCase.Response response)
    {
        throw new UnauthorizedAccessException("Not implemented....");
    }
}
