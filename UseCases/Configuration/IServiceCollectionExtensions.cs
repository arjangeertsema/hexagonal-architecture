using Common.CQRS.Abstractions;
using Common.IAM.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCasesServices(this IServiceCollection services, IConfiguration configuration) 
        {
            var assembly = typeof(IServiceCollectionExtensions).Assembly;

            return services
                .AutowireCQRS(assembly)
                .AutowireIAM(assembly);
        }
    }
}