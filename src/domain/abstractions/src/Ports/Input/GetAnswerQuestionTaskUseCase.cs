using System;

namespace Reference.Domain.Abstractions.Ports.Input
{        
    public class GetAnswerQuestionTaskUseCase : IInputPort<GetAnswerQuestionTaskUseCase.Response>
    {
        public GetAnswerQuestionTaskUseCase(long taskId)
        {
            TaskId = taskId;
        }

        public long TaskId { get; }

        public class Response
        {
            public Response(Guid QuestionId, long taskId, DateTime askedOn, string askedBy, string subject, string question)
            {
                if (string.IsNullOrEmpty(askedBy))
                {
                    throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or empty.", nameof(askedBy));
                }

                if (string.IsNullOrEmpty(subject))
                {
                    throw new ArgumentException($"'{nameof(subject)}' cannot be null or empty.", nameof(subject));
                }

                if (string.IsNullOrEmpty(question))
                {
                    throw new ArgumentException($"'{nameof(question)}' cannot be null or empty.", nameof(question));
                }

                this.QuestionId = QuestionId;
                TaskId = taskId;
                AskedOn = askedOn;
                AskedBy = askedBy;
                Subject = subject;
                Question = question;
            }

            public Guid QuestionId { get; }
            public long TaskId { get; }
            public DateTime AskedOn { get; }
            public string AskedBy { get; }
            public string Subject { get; }
            public string Question { get; }
        }
    }
}
