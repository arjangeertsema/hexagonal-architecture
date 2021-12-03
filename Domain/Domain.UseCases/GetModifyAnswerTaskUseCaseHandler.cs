namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetModifyAnswerTaskUseCaseHandler : IQueryHandler<GetModifyAnswerTaskUseCase, GetModifyAnswerTaskUseCase.Response>
{
    private readonly IMediator mediator;

    public GetModifyAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    [ResponseNotDefault]
    public async Task<GetModifyAnswerTaskUseCase.Response> Handle(GetModifyAnswerTaskUseCase query, CancellationToken cancellationToken)
    {
        var userTask = await mediator.Ask(new GetUserTask(query.UserTaskId));
        var questionId = new QuestionId(userTask.ReferenceId);
        var question = await mediator.Ask(new GetQuestion(questionId));

        return Map(userTask, question);
    }

    private GetModifyAnswerTaskUseCase.Response Map(GetUserTask.Response userTask, GetQuestion.Response answerQuestion)
    {
        return new GetModifyAnswerTaskUseCase.Response
        (
            questionId: answerQuestion.QuestionId,
            userTaskId: userTask.UserTaskId,
            userTaskClaim: userTask.UserTaskClaim,
            askedOn: answerQuestion.Asked,
            askedBy: answerQuestion.AskedBy,
            subject: answerQuestion.Subject,
            question: answerQuestion.Question,
            answer: answerQuestion.Answer,
            rejection: answerQuestion.Rejection
        );
    }
}
