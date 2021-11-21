var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCommonCQRSServices()
    .AddCommonDDDServices()
    .AddCommonIAMServices(builder.Configuration)
    .AddCommonUserTasksServices()

    // Add Domain implementations
    .AddDomainCoreServices()
    .AddDomainUseCasesServices()

    // Add Adapter implementations
    .AddEFAdapterServices(builder.Configuration)
    .AddRestAdapterServices()
    .AddSMTPAdapterServices(builder.Configuration)
    .AddZeebeAdapterServices(builder.Configuration);

var app = builder.Build();

app
    .ConfigureRestAdapter(builder.Configuration, app.Environment);

app.Run();
