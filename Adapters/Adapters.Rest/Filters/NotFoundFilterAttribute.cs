namespace Adapters.Rest.Filters;

public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is QueryResponseIsDefaultResponseException ||
            context.Exception is AggregateRootNotFoundException)
        {
            context.Result = new NotFoundResult();
        }
    }
}