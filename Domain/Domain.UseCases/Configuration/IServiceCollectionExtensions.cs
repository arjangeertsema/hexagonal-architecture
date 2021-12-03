namespace Domain.UseCases.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddUseCasesDomainServices(this IServiceCollection services)
    {
        var assembly = typeof(IServiceCollectionExtensions).Assembly;

        return services
            .AutowireCQRS(assembly)
            .AutowireIAM(assembly);
    }
}
