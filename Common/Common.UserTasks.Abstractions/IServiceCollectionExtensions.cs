using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;

namespace Common.UserTasks.Abstractions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AutowireUserTasks(this IServiceCollection services, Assembly assembly)
        {            
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly);
        }
    }
}
