using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Domain.UseCases.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDomainUseCasesServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddSingleton<IAcceptAnswerUseCase, AcceptAnswerUseCase>()
                .AddSingleton<IAnswerQuestionUseCase, AnswerQuestionUseCase>()
                .AddSingleton<IEndQuestionUseCase, EndQuestionUseCase>()
                .AddSingleton<IGetAnswerQuestionTaskUseCase, GetAnswerQuestionTaskUseCase>()
                .AddSingleton<IGetModifyAnswerTaskUseCase, GetModifyAnswerTaskUseCase>()
                .AddSingleton<IGetQuestionsUseCase, GetQuestionsUseCase>()
                .AddSingleton<IGetReviewAnswerTaskUseCase, GetReviewAnswerTaskUseCase>()
                .AddSingleton<IModifyAnswerUseCase, ModifyAnswerUseCase>()
                .AddSingleton<IRegisterQuestionUseCase, RegisterQuestionUseCase>()
                .AddSingleton<IRejectAnswerUseCase, RejectAnswerUseCase>()
                .AddSingleton<ISendAnswerUseCase, SendAnswerUseCase>()
                .AddSingleton<ISendQuestionAnsweredEventUseCase, SendQuestionAnsweredEventUseCase>();
        }
    }
}