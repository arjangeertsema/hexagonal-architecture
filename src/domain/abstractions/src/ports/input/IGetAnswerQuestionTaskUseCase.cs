namespace example.domain.abstractions.ports.input
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
