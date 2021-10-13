using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeebe.Client.Bootstrap.Extensions;
using Common.CQRS.Abstractions;

namespace Adapters.Zeebe.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForZeebeAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
                .BootstrapZeebe(configuration.GetSection("ZeebeBootstrap"), typeof(IServiceCollectionExtensions).Assembly);
        }
    }
}