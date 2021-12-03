namespace Adapters.EF;

[ServiceLifetime(ServiceLifetime.Scoped)]
public class QuestionStore :
    IEventHandler<QuestionRecievedEvent>,
    IEventHandler<QuestionAnsweredEvent>,
    IEventHandler<AnswerRejectedEvent>,
    IEventHandler<AnswerAcceptedEvent>,
    IEventHandler<AnswerModifiedEvent>,
    IEventHandler<AnswerSentEvent>,
    IQueryHandler<GetQuestion, GetQuestion.Response>,
    IQueryHandler<GetQuestions, IEnumerable<GetQuestions.Response>>
{
    private readonly DbContextAdapter context;

    public QuestionStore(DbContextAdapter context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

    public Task<GetQuestion.Response> Handle(GetQuestion query, CancellationToken cancellationToken)
    {
        return context.AnswerQuestions
            .Where(m => m.Id.Equals(query.QuestionId.ToString()))
            .Select(m => MapToAnswerQuestion(m))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<GetQuestions.Response>> Handle(GetQuestions query, CancellationToken cancellationToken)
    {
        var results = await context.AnswerQuestions
            .Skip(query.Offset)
            .Take(query.Limit)
            .Select(m => MapToAnswerQuestions(m))
            .ToListAsync(cancellationToken);

        return results;
    }

    public async Task Handle(QuestionRecievedEvent @event, CancellationToken cancellationToken)
    {
        var model = new AnswerQuestion()
        {
            Id = @event.AggregateRootId.ToString(),
            Asked = @event.Asked,
            AskedBy = @event.AskedBy,
            Subject = @event.Subject,
            Question = @event.Question
        };

        await context.AnswerQuestions.AddAsync(model, cancellationToken);
    }

    public async Task Handle(QuestionAnsweredEvent @event, CancellationToken cancellationToken)
    {
        var model = await context.AnswerQuestions.SingleAsync(m => m.Id.Equals(@event.AggregateRootId.ToString()));

        model.Answer = @event.Answer;
        model.Answered = @event.Answered;
        model.AnsweredBy = @event.AnsweredBy;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(AnswerRejectedEvent @event, CancellationToken cancellationToken)
    {
        var model = await context.AnswerQuestions.SingleAsync(m => m.Id.Equals(@event.AggregateRootId.ToString()));

        model.Rejection = @event.Rejection;
        model.Rejected = @event.Rejected;
        model.RejectedBy = @event.RejectedBy;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(AnswerAcceptedEvent @event, CancellationToken cancellationToken)
    {
        var model = await context.AnswerQuestions.SingleAsync(m => m.Id.Equals(@event.AggregateRootId.ToString()));

        model.Accepted = @event.Accepted;
        model.AcceptedBy = @event.AcceptedBy;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async  Task Handle(AnswerModifiedEvent @event, CancellationToken cancellationToken)
    {
        var model = await context.AnswerQuestions.SingleAsync(m => m.Id.Equals(@event.AggregateRootId.ToString()));

        model.Answer = @event.Answer;
        model.Modified = @event.Modified;
        model.ModifiedBy = @event.ModifiedBy;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(AnswerSentEvent @event, CancellationToken cancellationToken)
    {
        var model = await context.AnswerQuestions.SingleAsync(m => m.Id.Equals(@event.AggregateRootId.ToString()));

        model.Sent = @event.Sent;

        await context.SaveChangesAsync(cancellationToken);
    }

    private GetQuestion.Response MapToAnswerQuestion(AnswerQuestion answerQuestion)
    {
        if (answerQuestion == null)
            return null;

        return new GetQuestion.Response
        (
            questionId: new Domain.Abstractions.QuestionId(Guid.Parse(answerQuestion.Id)),
            asked: answerQuestion.Asked,
            askedBy: answerQuestion.AskedBy,
            subject: answerQuestion.Subject,
            question: answerQuestion.Question,
            answer: answerQuestion.Answer,
            rejection: answerQuestion.Rejection,
            lastActivity: GetLastActivity(answerQuestion),
            status: GetStatus(answerQuestion)
        );
    }

    private GetQuestions.Response MapToAnswerQuestions(AnswerQuestion answerQuestion)
    {
        if(answerQuestion == null)
            return null;

        return new GetQuestions.Response
        (
            questionId: new Domain.Abstractions.QuestionId(Guid.Parse(answerQuestion.Id)),
            asked: answerQuestion.Asked,
            askedBy: answerQuestion.AskedBy,
            subject: answerQuestion.Subject,
            question: answerQuestion.Question,
            answer: answerQuestion.Answer,
            rejection: answerQuestion.Rejection,
            lastActivity: GetLastActivity(answerQuestion),
            status: GetStatus(answerQuestion)
        );
    }

    private DateTime GetLastActivity(AnswerQuestion answerQuestion)
    {
        return new List<DateTime?>()
        {
            answerQuestion.Asked,
            answerQuestion.Answered,
            answerQuestion.Rejected,
            answerQuestion.Accepted,
            answerQuestion.Modified,
            answerQuestion.Sent,
        }
        .Where(d => d.HasValue)
        .Select(d => d.Value)
        .Max();
    }

    private AnswerQuestionStatus GetStatus(AnswerQuestion answerQuestion)
    {
        return new Dictionary<AnswerQuestionStatus, DateTime?>()
        {
            { AnswerQuestionStatus.ASKED, answerQuestion.Asked },
            { AnswerQuestionStatus.ANSWERED, answerQuestion.Answered },
            { AnswerQuestionStatus.REJECTED, answerQuestion.Rejected },
            { AnswerQuestionStatus.ACCEPTED, answerQuestion.Accepted },
            { AnswerQuestionStatus.MODIFIED, answerQuestion.Modified },
            { AnswerQuestionStatus.SENT, answerQuestion.Sent },
        }
        .Where(kvp => kvp.Value.HasValue)
        .OrderByDescending(kvp => kvp.Value)
        .Select(kvp => kvp.Key)
        .First();
    }
}
