using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.DDD.Abstractions;
using Common.CQRS.Abstractions;

namespace Adapters.Storage.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForStorageAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
                .AddTransient(typeof(IAggregateRootStore<>), typeof(AggregateRootStore<>));
        }
    }
}