namespace example.domain.abstractions.ports.input
{
    public interface IGetReviewAnswerTaskUseCase : IInputPort<IGetReviewAnswerTaskUseCase.Query, IGetReviewAnswerTaskUseCase.Response> 
    {        
        class Query : IQuery<Response>
        {
            
        }

        struct Response
        {
            
        }
    }
}
