namespace Adapters.Rest;

public class QuestionsApi : Generated.Controllers.QuestionsApiController
{
    private readonly IMediator mediator;

    public QuestionsApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> RegisterQuestion([FromBody] RegisterQuestion registerQuestion)
    {
        var command = new RegisterQuestionUseCase
        (
            commandId: registerQuestion.CommandId,
            questionId: new AnswerQuestionId(registerQuestion.QuestionId),
            subject: registerQuestion.Subject,
            question: registerQuestion.Question,
            askedBy: registerQuestion.Sender
        );
        await this.mediator.Send(command);
        return CreatedAtAction(nameof(GetQuestion), new { question_id = registerQuestion.QuestionId });
    }

    public override async Task<IActionResult> GetQuestions([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit)
    {
        var query = new GetQuestionsUseCase
        (
            offset: offset,
            limit: limit
        );

        var response = await mediator.Ask(query);
        return Ok(Map(response));
    }
    public override async Task<IActionResult> GetQuestion([FromRoute(Name = "question_id"), Required] Guid questionId)
    {
        var query = new GetQuestionUseCase
        (
            questionId: new AnswerQuestionId(questionId)
        );

        var response = await mediator.Ask(query);
        return Ok(Map(response));
    }

    public override async Task<IActionResult> EndQuestion([FromRoute(Name = "question_id"), Required] Guid questionId, [FromBody] EndQuestion endQuestion)
    {
        var command = new EndQuestionUseCase
        (
            commandId: endQuestion.CommandId,
            questionId: new AnswerQuestionId(questionId)
        );

        await this.mediator.Send(command);
        return Accepted();
    }

    private static QuestionsResponse Map(GetQuestionsUseCase.Response response)
    {
        return new QuestionsResponse()
        {
            Items = response.Items.Select(Map).ToList()
        };
    }

    private static QuestionsModel Map(GetQuestionsUseCase.Response.Item item)
    {
        return new QuestionsModel()
        {
            QuestionId = item.QuestionId.Id,
            RecievedOn = item.AskedOn,
            LastActivityOn = item.LastActivityOn,
            Subject = item.Subject,
            Sender = item.AskedBy,
            Status = (QuestionsModel.StatusEnum)Enum.Parse(typeof(QuestionsModel.StatusEnum), item.Status.ToString())
        };
    }

    private static QuestionResponse Map(GetQuestionUseCase.Response response)
    {
        return new QuestionResponse()
        {
            Question = Map((GetQuestionsUseCase.Response.Item)response)
        };
    }
}
