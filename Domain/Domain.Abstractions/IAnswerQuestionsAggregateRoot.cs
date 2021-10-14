using Common.DDD.Abstractions;

namespace Domain.Abstractions
{
    public interface IAnswerQuestionsAggregateRoot : IAggregateRoot
    {
        void AcceptAnswer(string userTaskId, string acceptedBy);
        void AnswerQuestion(string userTaskId, string answer, string answeredBy);
        void EndQuestion(string endedBy);
        void ModifyAnswer(string userTaskId, string answer, string modifiedBy);
        void RejectAnswer(string userTaskId, string rejection, string rejectedBy);
        void SendAnswer();
        void SendQuestionAnsweredEvent();
    }
}