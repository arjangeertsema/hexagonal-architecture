using Common.CQRS.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForUseCases(this IServiceCollection services, IConfiguration configuration) 
        {
            return services.AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly);
        }
    }
}