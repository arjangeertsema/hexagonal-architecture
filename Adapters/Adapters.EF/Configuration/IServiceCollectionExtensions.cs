using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;

namespace Adapters.EF.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEFAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly);
        }
    }
}