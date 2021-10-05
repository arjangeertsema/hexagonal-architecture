using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS;
using Zeebe.Client.Bootstrap.Extensions;

namespace Reference.Adapters.Zeebe.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForZeebeAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddMediator(typeof(IServiceCollectionExtensions))
                .BootstrapZeebe
                (
                    configuration.GetSection("ZeebeBootstrap"),
                    "Reference.Adapters.Zeebe"
                );
        }
    }
}