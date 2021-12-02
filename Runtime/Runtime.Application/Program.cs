var builder = WebApplication.CreateBuilder(args);

builder.Services
    // Add common services
    .AddCQRSCommonServices()    
    .AddDDDCommonServices()    
    .AddIAMCommonServices(builder.Configuration)
    .AddUserTasksCommonServices(builder.Configuration)

    // Add common adapters
    .AddEFCQRSCommonServices(builder.Configuration)
    .AddEFDDDCommonServices(builder.Configuration)
    .AddZeebeUserTasksCommonServices(builder.Configuration)

    // Add Domain implementations
    .AddDCoreDomainServices()
    .AddDUseCasesDomainServices()

    // Add Adapter implementations
    .AddEFAdapterServices(builder.Configuration)
    .AddRestAdapterServices()
    .AddSMTPAdapterServices(builder.Configuration)
    .AddZeebeAdapterServices(builder.Configuration);

var app = builder.Build();

app
    .ConfigureRestAdapter(builder.Configuration, app.Environment);

app.Run();
