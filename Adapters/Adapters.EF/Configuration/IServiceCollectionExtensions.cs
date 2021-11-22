namespace Adapters.EF.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEFAdapterServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
            .AddDbContext<DBContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
    }
}
