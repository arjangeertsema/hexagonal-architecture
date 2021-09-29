namespace Reference.Domain.Abstractions.Ports.Input
{
    public interface IGetAnswerQuestionTaskUseCase : IInputPort<IGetAnswerQuestionTaskUseCase.Query, IGetAnswerQuestionTaskUseCase.Response> 
    {        
        class Query : IQuery<Response>
        {
            
        }

        struct Response
        {
            
        }
    }
}
