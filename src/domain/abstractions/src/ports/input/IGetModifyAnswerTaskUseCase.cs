namespace example.domain.abstractions.ports.input
{
    public interface IGetModifyAnswerTaskUseCase : IInputPort<IGetModifyAnswerTaskUseCase.Query, IGetModifyAnswerTaskUseCase.Response> 
    {        
        class Query : IQuery<Response>
        {
            
        }

        struct Response
        {
            
        }
    }
}
