using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Aspects;
using Common.CQRS.Abstractions.Aspects;

namespace Common.CQRS
{
    public static class IServiceCollectionExtensions
    {
        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMediator, Mediator>()
                
                .AddScoped(typeof(AspectCommandHandler<>))
                .AddScoped(typeof(AspectEventHandler<>))
                .AddScoped(typeof(AspectQueryHandler<,>))

                .AddScoped(typeof(ICommandAspect<>), typeof(CommandAttributeAspect<>))                
                .AddScoped(typeof(IEventAspect<>), typeof(EventAttributeAspect<>))                
                .AddScoped(typeof(IQueryAspect<,>), typeof(QueryAttributeAspect<,>));
        }
    }
}
