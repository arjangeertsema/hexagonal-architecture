using Adapters.Zeebe.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Adapters.Rest.Configuration;
using UseCases.Configuration;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                //.AddServicesForStorageAdapter(Configuration)
                .AddServicesForZeebeAdapter(Configuration)
                .AddServicesForRestAdapter(Configuration)
                .AddServicesForUseCases(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureRestAdapter(Configuration, env);
        }
    }
}
