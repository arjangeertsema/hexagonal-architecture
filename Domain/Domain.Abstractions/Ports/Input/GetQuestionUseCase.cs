using System;
using Synion.CQRS.Abstractions.Ports;

namespace Domain.Abstractions.Ports.Input
{
    public class GetQuestionUseCase : IInputPort<GetQuestionUseCase.Response>
    {
        public GetQuestionUseCase(Guid questionId)
        {
            this.QuestionId = questionId;
        }
        public Guid QuestionId { get; }

        public class Response : GetQuestionsUseCase.Response.Item
        {
            public Response(Guid questionId, string subject, string question, string askedBy, DateTime askedOn, DateTime lastActivityOn, int status) 
                : base(questionId, subject, question, askedBy, askedOn, lastActivityOn, status)
            { }
        }
    }
}