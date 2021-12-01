namespace Adapters.Rest;

public class ReviewAnswerTasksApi : Generated.Controllers.ReviewAnswerTasksApiController
{
    private readonly IMediator mediator;

    public ReviewAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetReviewAnswerTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var query = new GetReviewAnswerTaskUseCase
        (
            userTask: userTask
        );

        var response = await mediator.Ask(query);
        return Ok(Map(query.UserTask, response));
    }

    public override async Task<IActionResult> AcceptAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AcceptAnswer acceptAnswer)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var command = new AcceptAnswerUseCase
        (
            commandId: acceptAnswer.CommandId,
            questionId: new AnswerQuestionId(acceptAnswer.QuestionId),
            userTask: userTask
        );

        await mediator.Send(command);
        return this.Accepted();
    }

    public override async Task<IActionResult> RejectAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] RejectAnswer rejectAnswer)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var command = new RejectAnswerUseCase
        (
            commandId: rejectAnswer.CommandId,
            questionId: new AnswerQuestionId(rejectAnswer.QuestionId),
            userTask: userTask,
            rejection: rejectAnswer.Rejection
        );

        await mediator.Send(command);
        return this.Accepted();
    }

    private ReviewAnswerTask Map(IUserTask userTask, GetReviewAnswerTaskUseCase.Response response)
    {
        return new ReviewAnswerTask()
        {
            QuestionId = response.QuestionId.Id,
            UserTaskId = userTask.UserTaskId,
            RecievedOn = response.AskedOn,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy,
            Answer = response.Answer
        };
    }
}
