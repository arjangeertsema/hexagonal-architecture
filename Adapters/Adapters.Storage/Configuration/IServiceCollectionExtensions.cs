using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS;
using Synion.DDD.Abstractions;

namespace Example.Adapters.Storage.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForStorageAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddMediator(typeof(IServiceCollectionExtensions))
                .AddSingleton<IAggregateRootStore, AggregateRootStore>();
        }
    }
}