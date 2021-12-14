namespace Domain.Core;

public class DraftAnswer : IDraftAnswer
{
    private Action<IVersionedDomainEvent<QuestionId>> RaiseEvent;
    private QuestionId questionId;
    public string answer;
    private string answeredBy;
    private DateTime? answered;
    private string acceptedBy;
    private DateTime? accepted;
    private string rejection;
    private string rejectedBy;
    private DateTime? rejected;
    private string modifiedBy;
    private DateTime? modified;
    private DateTime? sent;

    public DraftAnswer(QuestionId questionId, Action<IVersionedDomainEvent<QuestionId>> raiseEventDelegate)
    {
        this.questionId = questionId;
        this.RaiseEvent = raiseEventDelegate;
    }

    public void Apply(QuestionAnsweredEvent @event)
    {
        answer = @event.Answer;
        answeredBy = @event.AnsweredBy;
        answered = @event.Answered;
    }

    public void Accept(string acceptedBy)
    {
        if (string.IsNullOrWhiteSpace(acceptedBy))
        {
            throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
        }

        if (!answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is not answered.");
        }

        if (sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has been sent.");
        }

        if (answeredBy == acceptedBy)
        {
            throw new AnswerQuestionsException("Answer may not be reviewed by the same person.");
        }

        RaiseEvent(new AnswerAcceptedEvent(questionId, acceptedBy, DateTime.Now));
    }

    public void Apply(AnswerAcceptedEvent @event)
    {
        accepted = @event.Accepted;
        acceptedBy = @event.AcceptedBy;
    }

    public void Reject(string rejection, string rejectedBy)
    {
        if (string.IsNullOrWhiteSpace(rejection))
        {
            throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
        }

        if (string.IsNullOrWhiteSpace(rejectedBy))
        {
            throw new ArgumentException($"'{nameof(rejectedBy)}' cannot be null or whitespace.", nameof(rejectedBy));
        }

        if (!answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is not answered.");
        }

        if (sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has been sent.");
        }

        if (answeredBy == rejectedBy)
        {
            throw new AnswerQuestionsException("Answer may not be reviewed by the same person.");
        }

        RaiseEvent(new AnswerRejectedEvent(questionId, rejection, rejectedBy, DateTime.Now));
    }

    public void Apply(AnswerRejectedEvent @event)
    {
        rejection = @event.Rejection;
        rejectedBy = @event.RejectedBy;
        rejected = @event.Rejected;
    }

    public void Modify(string answer, string modifiedBy)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
        }

        if (string.IsNullOrWhiteSpace(modifiedBy))
        {
            throw new ArgumentException($"'{nameof(modifiedBy)}' cannot be null or whitespace.", nameof(modifiedBy));
        }

        if (!answered.HasValue)
        {
            throw new AnswerQuestionsException("Question is not answered.");
        }

        if (sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has already been sent.");
        }

        if (!rejected.HasValue)
        {
            throw new AnswerQuestionsException("Answer has not been rejected.");
        }

        if (answeredBy != modifiedBy)
        {
            throw new AnswerQuestionsException("Answer must be modified by the same person.");
        }

        RaiseEvent(new AnswerModifiedEvent(questionId, answer, modifiedBy, DateTime.Now));
    }

    public void Apply(AnswerModifiedEvent @event)
    {
        answer = @event.Answer;
        modifiedBy = @event.ModifiedBy;
        modified = @event.Modified;
    }

    public Message Send()
    {
        if (!accepted.HasValue)
        {
            throw new AnswerQuestionsException("Answer is not accepted.");
        }

        if (sent.HasValue)
        {
            throw new AnswerQuestionsException("Answer has already been sent.");
        };

        RaiseEvent(new AnswerSentEvent(questionId, DateTime.Now));

        throw new NotImplementedException();
    }

    public void Apply(AnswerSentEvent @event)
    {
        sent = @event.Sent;
    }
}
