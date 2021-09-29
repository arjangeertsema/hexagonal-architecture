using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Reference.Adapters.Rest.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureRestAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllers();
            
            return services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
                });
        }
    }
}