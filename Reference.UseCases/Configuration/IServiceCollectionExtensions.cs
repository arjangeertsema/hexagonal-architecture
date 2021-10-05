using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Reference.UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDomainUseCasesServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services;
        }
    }
}