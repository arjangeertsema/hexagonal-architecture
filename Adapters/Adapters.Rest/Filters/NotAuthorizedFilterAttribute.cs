namespace Adapters.Rest.Filters;

public class NotAuthorizedFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is MissingPermissionsException)
        {
            context.Result = new ForbidResult();
        }
    }
}