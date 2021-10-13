using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Common.CQRS.Abstractions;

namespace Adapters.Rest.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForRestAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
                .AddControllers();
            
            return services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
                });
        }
    }
}