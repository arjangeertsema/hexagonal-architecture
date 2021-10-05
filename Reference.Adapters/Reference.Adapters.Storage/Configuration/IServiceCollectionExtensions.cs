using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synion.DDD.Abstractions;

namespace Example.Adapters.Storage.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureStorageAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddSingleton<IAggregateRootStore, AggregateRootStore>();
        }
    }
}