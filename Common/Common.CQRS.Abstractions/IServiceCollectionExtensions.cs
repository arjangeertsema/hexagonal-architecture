using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;
using Common.CQRS.Abstractions.Attributes;

namespace Common.CQRS.Abstractions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AutowireCQRS(this IServiceCollection services, Assembly assembly)
        {            
            return services
                .FindAndAddImplementations(assembly, typeof(ICommandAttributeBehaviour<,>))
                .FindAndAddImplementations(assembly, typeof(ICommandBehaviour<>))
                .FindAndAddImplementations(assembly, typeof(ICommandHandler<>))
                .FindAndAddImplementations(assembly, typeof(ICommandTransactionScopeFactory<>))                
                
                .FindAndAddImplementations(assembly, typeof(IQueryAttributeBehaviour<,,>))
                .FindAndAddImplementations(assembly, typeof(IQueryBehaviour<,>))
                .FindAndAddImplementations(assembly, typeof(IQueryHandler<,>))

                .FindAndAddImplementations(assembly, typeof(IEventAttributeBehaviour<,>))
                .FindAndAddImplementations(assembly, typeof(IEventBehaviour<>))
                .FindAndAddImplementations(assembly, typeof(IEventHandler<>));
        }

        public static IServiceCollection FindAndAddImplementations(this IServiceCollection services, Assembly assembly, Type genericServiceTypeDefinition)
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
    }
}
