namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetAnswerQuestionTaskUseCaseHandler : IQueryHandler<GetAnswerQuestionTaskUseCase, GetAnswerQuestionTaskUseCase.Response>
{
    private readonly IMediator mediator;

    public GetAnswerQuestionTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("ANSWER_QUESTION")]
    [IsUserTaskOwner]
    [ResponseNotDefault]
    public async Task<GetAnswerQuestionTaskUseCase.Response> Handle(GetAnswerQuestionTaskUseCase query, CancellationToken cancellationToken)
    {
        var userTask = await mediator.Ask(new GetUserTask(query.UserTaskId));
        var questionId = new AnswerQuestionId(userTask.ReferenceId);
        var answerQuestion = await mediator.Ask(new GetAnswerQuestion(questionId));

        return Map(userTask, answerQuestion);
    }

    private GetAnswerQuestionTaskUseCase.Response Map(GetUserTask.Response userTask, GetAnswerQuestion.Response answerQuestion)
    {
        return new GetAnswerQuestionTaskUseCase.Response
        (
            questionId: answerQuestion.QuestionId,
            userTaskId: userTask.UserTaskId,
            userTaskClaim: userTask.UserTaskClaim,
            asked: answerQuestion.Asked,
            askedBy: answerQuestion.AskedBy,
            subject: answerQuestion.Subject,
            question: answerQuestion.Question
        );
    }
}
