namespace Domain.UseCases;

[ServiceLifetime(ServiceLifetime.Singleton)]
public class GetReviewAnswerTaskUseCaseHandler : IQueryHandler<GetReviewAnswerTaskUseCase, GetReviewAnswerTaskUseCase.Response>
{
    private readonly IMediator mediator;

    public GetReviewAnswerTaskUseCaseHandler(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HasPermission("REVIEW_ANSWER")]
    [IsUserTaskOwner]
    [ResponseNotDefault]
    public async Task<GetReviewAnswerTaskUseCase.Response> Handle(GetReviewAnswerTaskUseCase query, CancellationToken cancellationToken)
    {
        var userTask = await mediator.Ask(new GetUserTask(query.UserTaskId));
        var questionId = new AnswerQuestionId(userTask.ReferenceId);
        var answerQuestion = await mediator.Ask(new GetAnswerQuestion(questionId));

        return Map(userTask, answerQuestion);
    }

    private GetReviewAnswerTaskUseCase.Response Map(GetUserTask.Response userTask, GetAnswerQuestion.Response answerQuestion)
    {
        return new GetReviewAnswerTaskUseCase.Response
        (
            questionId: answerQuestion.QuestionId,
            userTaskId: userTask.UserTaskId,
            userTaskClaim: userTask.UserTaskClaim,
            askedOn: answerQuestion.Asked,
            askedBy: answerQuestion.AskedBy,
            subject: answerQuestion.Subject,
            question: answerQuestion.Question,
            answer: answerQuestion.Answer
        );
    }
}
