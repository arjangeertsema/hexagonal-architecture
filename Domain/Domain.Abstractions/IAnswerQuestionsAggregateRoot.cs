namespace Domain.Abstractions;

public interface IAnswerQuestionsAggregateRoot : IEventSourcedAggregateRoot<AnswerQuestionId>
{
    void AcceptAnswer(IUserTask userTask, string acceptedBy);
    void AnswerQuestion(IUserTask userTask, string answer, string answeredBy);
    void EndQuestion(string endedBy);
    void ModifyAnswer(IUserTask userTask, string answer, string modifiedBy);
    void RejectAnswer(IUserTask userTask, string rejection, string rejectedBy);
    Message SendAnswer();
    void SendQuestionAnsweredEvent();
}
