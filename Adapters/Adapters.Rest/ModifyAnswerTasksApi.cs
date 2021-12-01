namespace Adapters.Rest;

public class ModifyAnswerTasksApi : Generated.Controllers.ModifyAnswerTasksApiController
{
    private readonly IMediator mediator;

    public ModifyAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetModifyAnswerTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var query = new GetModifyAnswerTaskUseCase
        (
            userTask: userTask
        );

        var response = await this.mediator.Ask(query);
        return Ok(Map(query.UserTask, response));
    }

    public override async Task<IActionResult> ModifyAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] ModifyAnswer modifyAnswer)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var command = new ModifyAnswerUseCase
        (
            commandId: modifyAnswer.CommandId,
            questionId: new AnswerQuestionId(modifyAnswer.QuestionId),
            userTask: userTask,
            answer: modifyAnswer.Answer
        );

        await this.mediator.Send(command);
        return this.Accepted();
    }

    private ModifyAnswerTask Map(IUserTask userTask, GetModifyAnswerTaskUseCase.Response response)
    {
        return new ModifyAnswerTask()
        {
            QuestionId = response.QuestionId.Id,
            UserTaskId = userTask.UserTaskId,
            RecievedOn = response.AskedOn,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy,
            Answer = response.Answer,
            Rejection = response.Rejection
        };
    }
}
