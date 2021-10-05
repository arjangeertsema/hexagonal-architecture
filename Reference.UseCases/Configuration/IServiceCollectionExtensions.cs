using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS;

namespace Reference.UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForUseCases(this IServiceCollection services, IConfiguration configuration) 
        {
            return services.AddMediator(typeof(IServiceCollectionExtensions));
        }
    }
}