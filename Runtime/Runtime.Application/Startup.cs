namespace Runtime.Application;
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
            .AddEFAdapterServices(Configuration)
            .AddRestAdapterServices()
            .AddSMTPAdapterServices(Configuration)
            .AddZeebeAdapterServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app
            .ConfigureRestAdapter(Configuration, env);
    }
}

