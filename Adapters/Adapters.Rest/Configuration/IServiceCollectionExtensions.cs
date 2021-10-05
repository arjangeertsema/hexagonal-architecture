using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Synion.CQRS;

namespace Adapters.Rest.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForRestAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            services
                .AddMediator(typeof(IServiceCollectionExtensions))
                .AddControllers();
            
            return services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
                });
        }
    }
}