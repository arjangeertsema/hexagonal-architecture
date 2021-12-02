namespace Adapters.Rest;

public class AnswerQuestionTasksApi : Generated.Controllers.AnswerQuestionTasksApiController
{
    private readonly IMediator mediator;

    public AnswerQuestionTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetAnswerQuestionTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId));

        var query = new GetAnswerQuestionTaskUseCase
        (
            userTaskId: _userTaskId
        );

        var response = await this.mediator.Ask(query);
        return Ok(Map(query.UserTaskId, response));
    }
    
    public override async Task<IActionResult> AnswerQuestion([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AnswerQuestion answerQuestion)
    {
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId));

        var command = new AnswerQuestionUseCase
        (
            commandId: answerQuestion.CommandId,
            questionId: new AnswerQuestionId(answerQuestion.QuestionId),
            userTaskId: _userTaskId, 
            answer: answerQuestion.Answer
        );

        await this.mediator.Send(command);
        return this.Accepted();
    }

    private AnswerQuestionTask Map(IUserTaskId userTaskId, GetAnswerQuestionTaskUseCase.Response response)
    {
        return new AnswerQuestionTask()
        {
            UserTaskId = userTaskId.Id,
            QuestionId = response.QuestionId.Id,
            RecievedOn = response.AskedOn,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy
        };
    }
}
