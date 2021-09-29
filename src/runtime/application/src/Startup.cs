using Reference.Adapters.Rest.Configuration;
using Reference.Adapters.Zeebe.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Example.Adapters.DDD.Configuration;
using Reference.Domain.UseCases.Configuration;

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
                .ConfigureDDDAdapterServices(Configuration)
                .ConfigureZeebeAdapterServices(Configuration)
                .ConfigureRestAdapterServices(Configuration)
                .ConfigureDomainUseCasesServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureRestAdapter(Configuration, env);
        }
    }
}
