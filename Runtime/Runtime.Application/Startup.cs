using Adapters.Zeebe.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Adapters.Rest.Configuration;
using Domain.UseCases.Configuration;
using Common.CQRS.Configuration;
using Common.IAM.Configuration;
using Common.UserTasks.Configuration;
using Domain.Core.Configuration;
using Common.DDD.Configuration;

namespace Runtime.Application
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
                // Add Common implementations
                .AddCommonCQRSServices()
                .AddCommonDDDServices()
                .AddCommonIAMServices(Configuration)
                .AddCommonUserTasksServices()
                
                // Add Domain implementations
                .AddDomainCoreServices()
                .AddDomainUseCasesServices()
                
                // Add Adapter implementations
                .AddZeebeAdapterServices(Configuration)
                .AddRestAdapterServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .ConfigureRestAdapter(Configuration, env);
        }
    }
}
