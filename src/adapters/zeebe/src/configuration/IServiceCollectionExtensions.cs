using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeebe.Client.Bootstrap.Extensions;

namespace example.adapters.zeebe.configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureZeebeAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services.BootstrapZeebe
            (
                configuration.GetSection("ZeebeBootstrap"),
                "example.adapters.zeebe"
            );
        }
    }
}