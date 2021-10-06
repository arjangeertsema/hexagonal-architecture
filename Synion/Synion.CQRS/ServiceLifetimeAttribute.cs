using System;
using Microsoft.Extensions.DependencyInjection;

namespace Synion.CQRS
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class ServiceLifetimeAttribute : Attribute
    {
        public ServiceLifetimeAttribute(ServiceLifetime serviceLifetime)
        {            
            this.ServiceLifetime = serviceLifetime;
        }

        public ServiceLifetime ServiceLifetime { get; }
    }
}