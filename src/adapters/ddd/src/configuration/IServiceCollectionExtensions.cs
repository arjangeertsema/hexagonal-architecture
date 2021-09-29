using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions.Ports.Output;

namespace Example.Adapters.DDD.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDDDAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddSingleton<ISaveAggregateRootPort, AggregateRootService>();
        }
    }
}