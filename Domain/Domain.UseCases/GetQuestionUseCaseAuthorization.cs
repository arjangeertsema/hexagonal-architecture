namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetQuestionUseCaseAuthorization : IQueryAuthorization<GetQuestionUseCase, GetQuestionUseCase.Response>
{
    public Task Authorize(string userId, GetQuestionUseCase query, GetQuestionUseCase.Response response)
    {
        throw new UnauthorizedAccessException("Not implemented....");
    }
}
