using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS;

namespace UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForUseCases(this IServiceCollection services, IConfiguration configuration) 
        {
            return services.AddMediator(typeof(IServiceCollectionExtensions).Assembly);
        }
    }
}