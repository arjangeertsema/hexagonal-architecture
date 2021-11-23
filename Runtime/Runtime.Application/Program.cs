var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCQRSCommonServices()
    .AddEFCQRSCommonServices(builder.Configuration)
    .AddDDDCommonServices()
    .AddEFDDDCommonServices(builder.Configuration)
    .AddIAMCommonServices(builder.Configuration)
    .AddUserTasksCommonServices()
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
