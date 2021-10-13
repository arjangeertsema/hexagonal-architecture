using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Common.IAM.Abstractions.Commands;
using Common.IAM.Abstractions.Queries;
using Common.IAM.Abstractions.Events;
using Common.CQRS.Abstractions;

namespace Common.IAM.Abstractions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AutowireIAM(this IServiceCollection services, Assembly assembly)
        {            
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
                .FindAndAddImplementations(assembly, typeof(ICommandAuthorization<>))                
                .FindAndAddImplementations(assembly, typeof(IQueryAuthorization<,>))
                .FindAndAddImplementations(assembly, typeof(IEventAuthorization<>));
        }
    }
}
