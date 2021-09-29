using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeebe.Client.Bootstrap.Extensions;

namespace Reference.Adapters.Zeebe.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureZeebeAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services.BootstrapZeebe
            (
                configuration.GetSection("ZeebeBootstrap"),
                "Reference.Adapters.Zeebe"
            );
        }
    }
}