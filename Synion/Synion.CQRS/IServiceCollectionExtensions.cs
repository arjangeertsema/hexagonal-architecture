using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Synion.CQRS.Abstractions;
using Synion.CQRS.Abstractions.Commands;
using Synion.CQRS.Abstractions.Events;
using Synion.CQRS.Abstractions.Ports;
using Synion.CQRS.Abstractions.Queries;
using Synion.CQRS.Commands;
using Synion.CQRS.Events;
using Synion.CQRS.Queries;

namespace Synion.CQRS
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
        {            
            return services
                .FindAndAddImplementations(assembly, typeof(ICommandAttributeBehaviour<,>))
                .FindAndAddImplementations(assembly, typeof(ICommandAuthorization<>))
                .FindAndAddImplementations(assembly, typeof(ICommandBehaviour<>))
                .FindAndAddImplementations(assembly, typeof(ICommandHandler<>))
                .FindAndAddImplementations(assembly, typeof(ICommandTransactionScopeFactory<>))
                
                
                .FindAndAddImplementations(assembly, typeof(IQueryAttributeBehaviour<,,>))
                .FindAndAddImplementations(assembly, typeof(IQueryAuthorization<,>))
                .FindAndAddImplementations(assembly, typeof(IQueryBehaviour<,>))
                .FindAndAddImplementations(assembly, typeof(IQueryHandler<,>))

                .FindAndAddImplementations(assembly, typeof(IEventAttributeBehaviour<,>))
                .FindAndAddImplementations(assembly, typeof(IEventAuthorization<>))
                .FindAndAddImplementations(assembly, typeof(IEventBehaviour<>))
                .FindAndAddImplementations(assembly, typeof(IEventHandler<>))

                .FindAndAddImplementations(assembly, typeof(IInputPortHandler<>))
                .FindAndAddImplementations(assembly, typeof(IInputPortHandler<,>))
                .FindAndAddImplementations(assembly, typeof(IOutputPortHandler<>))
                .FindAndAddImplementations(assembly, typeof(IOutputPortHandler<,>))                
                
                .AddMediator();            
        }

        private static IServiceCollection FindAndAddImplementations(this IServiceCollection services, Assembly assembly, Type genericServiceTypeDefinition)
        {
            var types = assembly
                .GetTypes()
                .Where(t => ImplementsGenericTypeDefinition(t, genericServiceTypeDefinition));

            foreach (var type in types)
            {
                var serviceLifetime = GetServiceLifetime(type);

                if(type.IsGenericType)
                {
                    AddGenericServiceDefinition(services, genericServiceTypeDefinition, type, serviceLifetime);
                }
                else
                {
                    AddGenericServiceImplementation(services, genericServiceTypeDefinition, type, serviceLifetime);
                }
            }
                
            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            if(!services.IsAlreadyRegistered(typeof(IMediator)))
                services.AddSingleton<IMediator, Mediator>();

            if(services.IsAlreadyRegistered(typeof(BehaviourCommandHandler<>)))
                services.AddTransient(typeof(BehaviourCommandHandler<>));

            if(services.IsAlreadyRegistered(typeof(CommandAttributeBehaviour<>)))
                services.AddTransient(typeof(ICommandBehaviour<>), typeof(CommandAttributeBehaviour<>));

            if(services.IsAlreadyRegistered(typeof(BehaviourEventHandler<>)))
                services.AddTransient(typeof(BehaviourEventHandler<>));

            if(services.IsAlreadyRegistered(typeof(EventAttributeBehaviour<>)))
                services.AddTransient(typeof(IEventBehaviour<>), typeof(EventAttributeBehaviour<>));

            if(services.IsAlreadyRegistered(typeof(BehaviourQueryHandler<,>)))
                services.AddTransient(typeof(BehaviourQueryHandler<,>));

            if(services.IsAlreadyRegistered(typeof(QueryAttributeBehaviour<,>)))
                services.AddTransient(typeof(IQueryBehaviour<,>), typeof(QueryAttributeBehaviour<,>));

            return services;
        }

        private static void AddGenericServiceDefinition(IServiceCollection services, Type genericServiceTypeDefinition, Type genericImplementationTypeDefinition, ServiceLifetime serviceLifetime)
        {
            services.Add(new ServiceDescriptor(genericServiceTypeDefinition, genericImplementationTypeDefinition, serviceLifetime));
        }

        private static void AddGenericServiceImplementation(IServiceCollection services, Type genericServiceTypeDefinition, Type implementationType, ServiceLifetime serviceLifetime)
        {
            var serviceType = GetGetGenericType(implementationType, genericServiceTypeDefinition);
            services.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));
        }

        private static bool ImplementsGenericTypeDefinition(Type type, Type genericTypeDefinition)
        {
            if(type.IsAbstract || type.IsInterface || type.IsNotPublic)
                return false;

            return type
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(genericTypeDefinition));
        }

        private static Type GetGetGenericType(Type type, Type genericTypeDefinition)
        {
            return type 
                .GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(genericTypeDefinition));
        }

        private static ServiceLifetime GetServiceLifetime(Type type)
        {
            var attr = type.GetCustomAttribute<ServiceLifetimeAttribute>();
            if(attr == null)
                return ServiceLifetime.Transient;

            return attr.ServiceLifetime;
        }

        private static bool IsAlreadyRegistered(this IServiceCollection services, Type serviceType)
        {
            return services.Any(s => s.ServiceType == serviceType);
        }
    }
}
