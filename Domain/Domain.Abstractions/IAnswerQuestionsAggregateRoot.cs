namespace Domain.Abstractions;

public interface IAnswerQuestionsAggregateRoot : IEventSourcedAggregateRoot<AnswerQuestionId>
{
    void AnswerQuestion(IUserTask userTask, string answer, string answeredBy);
    void AcceptAnswer(IUserTask userTask, string acceptedBy);
    void RejectAnswer(IUserTask userTask, string rejection, string rejectedBy);    
    void ModifyAnswer(IUserTask userTask, string answer, string modifiedBy);    
    Message SendAnswer();
    QuestionAnswerdIntegrationEvent SendEvent();
    void EndQuestion(string endedBy);
}
