namespace Adapters.Zeebe.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddZeebeAdapterServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
            .BootstrapZeebe(configuration.GetSection("ZeebeBootstrap"), typeof(IServiceCollectionExtensions).Assembly);
    }
}
