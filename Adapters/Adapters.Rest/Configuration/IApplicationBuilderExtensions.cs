using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Adapters.Rest.Configuration
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureRestAdapter(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "application v1"));
            }

            return app
                .UseHttpsRedirection()
                .UseRouting()
                
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}