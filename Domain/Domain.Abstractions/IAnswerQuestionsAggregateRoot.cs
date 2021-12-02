namespace Domain.Abstractions;

public interface IAnswerQuestionsAggregateRoot : IEventSourcedAggregateRoot<AnswerQuestionId>
{
    void AnswerQuestion(IUserTaskId userTaskId, string answer, string answeredBy);
    void AcceptAnswer(IUserTaskId userTaskId, string acceptedBy);
    void RejectAnswer(IUserTaskId userTaskId, string rejection, string rejectedBy);    
    void ModifyAnswer(IUserTaskId userTaskId, string answer, string modifiedBy);    
    Message SendAnswer();
    QuestionAnswerdIntegrationEvent SendEvent();
    void EndQuestion(string endedBy);
}
