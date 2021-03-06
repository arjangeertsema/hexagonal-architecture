namespace Adapters.Rest;

public class AnswerQuestionTasksApi : Generated.Controllers.AnswerQuestionTasksApiController
{
    private readonly IMediator mediator;

    public AnswerQuestionTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> GetAnswerQuestionTask([FromRoute(Name = "task_id"), Required] string userTaskId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var query = new GetAnswerQuestionTaskUseCase
        (
            userTaskId: _userTaskId
        );

        var response = await this.mediator.Ask(query, cancellationToken);

        return Ok(Map(response));
    }
    
    public override async Task<IActionResult> AnswerQuestion([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AnswerQuestion answerQuestion)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var _userTaskId = await mediator.Ask(new GetUserTaskId(userTaskId), cancellationToken);

        var command = new AnswerQuestionUseCase
        (
            commandId: answerQuestion.CommandId,
            questionId: new QuestionId(answerQuestion.QuestionId),
            userTaskId: _userTaskId, 
            answer: answerQuestion.Answer
        );

        await this.mediator.Send(command, cancellationToken);
        
        return this.Accepted();
    }

    private AnswerQuestionTask Map(GetAnswerQuestionTaskUseCase.Response response)
    {
        return new AnswerQuestionTask()
        {
            QuestionId = response.QuestionId.Id,
            UserTaskId = response.UserTaskId.Id,            
            RecievedOn = response.Asked,
            Subject = response.Subject,
            Question = response.Question,
            Sender = response.AskedBy
        };
    }
}
