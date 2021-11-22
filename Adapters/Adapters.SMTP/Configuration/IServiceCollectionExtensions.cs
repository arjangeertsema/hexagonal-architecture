namespace Adapters.SMTP.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSMTPAdapterServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
            .Configure<SMTPOptions>(configuration.GetSection("SMTP"));
    }
}
