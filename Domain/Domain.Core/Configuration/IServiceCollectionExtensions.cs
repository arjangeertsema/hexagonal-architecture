namespace Domain.Core.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDCoreDomainServices(this IServiceCollection services)
    {
        var assembly = typeof(IServiceCollectionExtensions).Assembly;

        return services
            .AutowireCQRS(assembly)
            .AutowireDDD(assembly);
    }
}
