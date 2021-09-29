namespace Reference.Domain.Abstractions.Ports.Input
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
