using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions.DDD;

namespace Example.Adapters.Storage.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureStoragesAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddSingleton<IAggregateRootStore, AggregateRootStore>();
        }
    }
}