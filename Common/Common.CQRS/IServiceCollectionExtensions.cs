using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;
using Common.CQRS.Commands;
using Common.CQRS.Events;
using Common.CQRS.Queries;

namespace Common.CQRS
{
    public static class IServiceCollectionExtensions
    {
        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMediator, Mediator>()
                .AddScoped(typeof(BehaviourCommandHandler<>))
                .AddScoped(typeof(ICommandBehaviour<>), typeof(CommandAttributeBehaviour<>))
                .AddScoped(typeof(BehaviourEventHandler<>))
                .AddScoped(typeof(IEventBehaviour<>), typeof(EventAttributeBehaviour<>))
                .AddScoped(typeof(BehaviourQueryHandler<,>))
                .AddScoped(typeof(IQueryBehaviour<,>), typeof(QueryAttributeBehaviour<,>));
        }
    }
}
