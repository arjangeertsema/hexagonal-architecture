using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.CQRS.Abstractions;

namespace Adapters.SMTP.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSMTPAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AutowireCQRS(typeof(IServiceCollectionExtensions).Assembly)
                .Configure<SMTPOptions>(configuration.GetSection("SMTP"));
        }
    }
}