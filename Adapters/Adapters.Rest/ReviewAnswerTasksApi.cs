namespace Adapters.Rest;

public class ReviewAnswerTasksApi : Generated.Controllers.ReviewAnswerTasksApiController
{
    private readonly IMediator mediator;

    public ReviewAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetReviewAnswerTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var query = new GetReviewAnswerTaskUseCase
        (
            userTaskId: _userTaskId
        );

        var response = await mediator.Ask(query, cancellationToken);
        
        return Ok(Map(response));
    }

    public override async Task<IActionResult> AcceptAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AcceptAnswer acceptAnswer)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var command = new AcceptAnswerUseCase
        (
            commandId: acceptAnswer.CommandId,
            questionId: new QuestionId(acceptAnswer.QuestionId),
            userTaskId: _userTaskId
        );

        await mediator.Send(command, cancellationToken);

        return this.Accepted();
    }

    public override async Task<IActionResult> RejectAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] RejectAnswer rejectAnswer)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var command = new RejectAnswerUseCase
        (
            commandId: rejectAnswer.CommandId,
            questionId: new QuestionId(rejectAnswer.QuestionId),
            userTaskId: _userTaskId,
            rejection: rejectAnswer.Rejection
        );

        await mediator.Send(command, cancellationToken);

        return this.Accepted();
    }

    private ReviewAnswerTask Map(GetReviewAnswerTaskUseCase.Response response)
    {
        return new ReviewAnswerTask()
        {
            QuestionId = response.QuestionId.Id,
            UserTaskId = response.UserTaskId.Id,
            RecievedOn = response.AskedOn,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy,
            Answer = response.Answer
        };
    }
}
