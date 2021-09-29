namespace Reference.Domain.Abstractions.Ports.Input
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
