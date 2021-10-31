using Common.DDD.Abstractions;
using Domain.Abstractions.ValueTypes;

namespace Domain.Abstractions
{
    public interface IAnswerQuestionsAggregateRoot : IAggregateRoot, IEventSourcedAggregateRoot
    {
        void AcceptAnswer(string userTaskId, string acceptedBy);
        void AnswerQuestion(string userTaskId, string answer, string answeredBy);
        void EndQuestion(string endedBy);
        void ModifyAnswer(string userTaskId, string answer, string modifiedBy);
        void RejectAnswer(string userTaskId, string rejection, string rejectedBy);
        Message SendAnswer();
        void SendQuestionAnsweredEvent();
    }
}