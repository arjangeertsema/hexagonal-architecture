using Common.CQRS.Abstractions;
using Common.IAM.Abstractions;
using Common.UserTasks.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForUseCases(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
                .AutowireIAM(typeof(IServiceCollectionExtensions).Assembly)
                .AutowireUserTasks(typeof(IServiceCollectionExtensions).Assembly);
        }
    }
}