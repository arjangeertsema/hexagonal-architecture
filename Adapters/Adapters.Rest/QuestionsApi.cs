using Domain.Abstractions.Enums;

namespace Adapters.Rest;

public class QuestionsApi : Generated.Controllers.QuestionsApiController
{
    private readonly IMediator mediator;

    public QuestionsApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> RegisterQuestion([FromBody] RegisterQuestion registerQuestion)
    {
        var cancellationToken = HttpContext.RequestAborted;

        var command = new AskQuestionUseCase
        (
            commandId: registerQuestion.CommandId,
            questionId: new QuestionId(registerQuestion.QuestionId),
            subject: registerQuestion.Subject,
            question: registerQuestion.Question,
            askedBy: registerQuestion.Sender
        );
        await this.mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetQuestion), new { question_id = registerQuestion.QuestionId });
    }

    public override async Task<IActionResult> GetQuestions([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetQuestionsUseCase
        (
            offset: offset ?? 0,
            limit: limit ?? 10
        );

        var response = await mediator.Ask(query, cancellationToken);

        return Ok(Map(response));
    }
    public override async Task<IActionResult> GetQuestion([FromRoute(Name = "question_id"), Required] Guid questionId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetQuestionUseCase
        (
            questionId: new QuestionId(questionId)
        );

        var response = await mediator.Ask(query, cancellationToken);

        return Ok(Map(response));
    }

    public override async Task<IActionResult> EndQuestion([FromRoute(Name = "question_id"), Required] Guid questionId, [FromBody] EndQuestion endQuestion)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new EndQuestionUseCase
        (
            commandId: endQuestion.CommandId,
            questionId: new QuestionId(questionId)
        );

        await this.mediator.Send(command, cancellationToken);

        return Accepted();
    }

    private static QuestionsResponse Map(IEnumerable<GetQuestionsUseCase.Response> response)
    {
        return new QuestionsResponse()
        {
            Items = response.Select(Map).ToList()
        };
    }

    private static QuestionsModel Map(GetQuestionsUseCase.Response item)
    {
        return new QuestionsModel()
        {
            QuestionId = item.QuestionId.Id,
            RecievedOn = item.Asked,
            LastActivityOn = item.LastActivity,
            Subject = item.Subject,
            Sender = item.AskedBy,
            Status = Map(item.Status)
        };
    }

    private static QuestionResponse Map(GetQuestionUseCase.Response response)
    {
        return new QuestionResponse()
        {
            Question = Map((GetQuestionsUseCase.Response)response)
        };
    }

    private static QuestionsModel.StatusEnum Map(AnswerQuestionStatus status)
    {
        switch(status)
        {
            case AnswerQuestionStatus.ASKED:
                return QuestionsModel.StatusEnum.ProcessStartedEnum;
            case AnswerQuestionStatus.ANSWERED:
                return QuestionsModel.StatusEnum.QuestionAnsweredEnum;
            case AnswerQuestionStatus.REJECTED:
                return QuestionsModel.StatusEnum.AnswerRejectedEnum;
            case AnswerQuestionStatus.MODIFIED:
                return QuestionsModel.StatusEnum.AnswerModifiedEnum;
            case AnswerQuestionStatus.ACCEPTED:
                return QuestionsModel.StatusEnum.AnswerAcceptedEnum;
            case AnswerQuestionStatus.SENT:
                return QuestionsModel.StatusEnum.AnswerSendEnum;
            default:
                throw new NotImplementedException();
        }
    }
}
