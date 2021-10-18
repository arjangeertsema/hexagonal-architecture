using Common.CQRS.Abstractions;
using Common.IAM.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainUseCasesServices(this IServiceCollection services) 
        {
            var assembly = typeof(IServiceCollectionExtensions).Assembly;

            return services
                .AutowireCQRS(assembly)
                .AutowireIAM(assembly);
        }
    }
}