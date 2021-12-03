namespace Adapters.Rest;

public class ModifyAnswerTasksApi : Generated.Controllers.ModifyAnswerTasksApiController
{
    private readonly IMediator mediator;

    public ModifyAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetModifyAnswerTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var query = new GetModifyAnswerTaskUseCase
        (
            userTaskId: _userTaskId
        );

        var response = await this.mediator.Ask(query, cancellationToken);

        return Ok(Map(response));
    }

    public override async Task<IActionResult> ModifyAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] ModifyAnswer modifyAnswer)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var command = new ModifyAnswerUseCase
        (
            commandId: modifyAnswer.CommandId,
            questionId: new QuestionId(modifyAnswer.QuestionId),
            userTaskId: _userTaskId,
            answer: modifyAnswer.Answer
        );

        await this.mediator.Send(command, cancellationToken);
        
        return this.Accepted();
    }

    private ModifyAnswerTask Map(GetModifyAnswerTaskUseCase.Response response)
    {
        return new ModifyAnswerTask()
        {
            QuestionId = response.QuestionId.Id,
            UserTaskId = response.UserTaskId.Id,
            RecievedOn = response.AskedOn,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy,
            Answer = response.Answer,
            Rejection = response.Rejection
        };
    }
}
