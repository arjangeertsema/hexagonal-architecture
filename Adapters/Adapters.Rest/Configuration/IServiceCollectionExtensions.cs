namespace Adapters.Rest.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRestAdapterServices(this IServiceCollection services)
    {
        services
            .AddControllers(config =>
            {
                config.Filters.Add(new NotImplExceptionFilterAttribute());
            });

        return services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
            });
    }
}
