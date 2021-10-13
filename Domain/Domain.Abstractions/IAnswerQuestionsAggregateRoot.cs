using Common.DDD.Abstractions;

namespace Domain.Abstractions
{
    public interface IAnswerQuestionsAggregateRoot : IAggregateRoot
    {
        void AcceptAnswer(long taskId, string acceptedBy);
        void AnswerQuestion(long taskId, string answer, string answeredBy);
        void EndQuestion(string endedBy);
        void ModifyAnswer(long taskId, string answer, string modifiedBy);
        void RejectAnswer(long taskId, string rejection, string rejectedBy);
        void SendAnswer();
        void SendQuestionAnsweredEvent();
    }
}