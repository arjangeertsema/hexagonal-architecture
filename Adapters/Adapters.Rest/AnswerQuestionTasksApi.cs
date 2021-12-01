namespace Adapters.Rest;

public class AnswerQuestionTasksApi : Generated.Controllers.AnswerQuestionTasksApiController
{
    private readonly IMediator mediator;

    public AnswerQuestionTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetAnswerQuestionTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var query = new GetAnswerQuestionTaskUseCase
        (
            userTask: userTask
        );

        var response = await this.mediator.Ask(query);
        return Ok(Map(query.UserTask, response));
    }
    
    public override async Task<IActionResult> AnswerQuestion([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AnswerQuestion answerQuestion)
    {
        var userTask = await mediator.Ask(new MapToUserTask(userTaskId));

        var command = new AnswerQuestionUseCase
        (
            commandId: answerQuestion.CommandId,
            questionId: new AnswerQuestionId(answerQuestion.QuestionId),
            userTask: userTask, 
            answer: answerQuestion.Answer
        );

        await this.mediator.Send(command);
        return this.Accepted();
    }

    private AnswerQuestionTask Map(IUserTask userTask, GetAnswerQuestionTaskUseCase.Response response)
    {
        return new AnswerQuestionTask()
        {
            UserTaskId = userTask.UserTaskId,
            QuestionId = response.QuestionId.Id,
            RecievedOn = response.AskedOn,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy
        };
    }
}
