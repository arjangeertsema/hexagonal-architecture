using System;
using Microsoft.Extensions.DependencyInjection;

namespace Synion.CQRS
{
     public class ServiceLifetimeAttribute : Attribute
    {
        public ServiceLifetimeAttribute(ServiceLifetime serviceLifetime)
        {            
            this.ServiceLifetime = serviceLifetime;
        }

        public ServiceLifetime ServiceLifetime { get; }
    }
}