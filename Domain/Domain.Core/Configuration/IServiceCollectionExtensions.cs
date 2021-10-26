using Common.DDD.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Core.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainCoreServices(this IServiceCollection services) 
        {
            var assembly = typeof(IServiceCollectionExtensions).Assembly;

            return services
                .AutowireDDD(assembly);
        }
    }
}