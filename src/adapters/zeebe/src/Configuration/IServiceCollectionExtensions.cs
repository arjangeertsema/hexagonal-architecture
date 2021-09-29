using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions.Ports.Output;
using Reference.Domain.Core.AnswerQuestions;
using Zeebe.Client.Bootstrap.Extensions;

namespace Reference.Adapters.Zeebe.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureZeebeAdapterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .BootstrapZeebe
                (
                    configuration.GetSection("ZeebeBootstrap"),
                    "Reference.Adapters.Zeebe"
                )
                .AddScoped<IHandleDomainEventPort<QuestionRecievedEvent>, AnswerQuestionsService>()
                .AddScoped<IHandleDomainEventPort<QuestionAnsweredEvent>, AnswerQuestionsService>()
                .AddScoped<IHandleDomainEventPort<AnswerRejectedEvent>, AnswerQuestionsService>()
                .AddScoped<IHandleDomainEventPort<AnswerAcceptedEvent>, AnswerQuestionsService>()
                .AddScoped<IHandleDomainEventPort<AnswerModifiedEvent>, AnswerQuestionsService>()
                .AddScoped<IGetAggregateRootStatePort, ProcessStateService>();
        }
    }
}