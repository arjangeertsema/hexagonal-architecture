using System;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports.Input
{        
    public class GetAnswerQuestionTaskUseCase : IQuery<GetAnswerQuestionTaskUseCase.Response>, IUserTask
    {
        public GetAnswerQuestionTaskUseCase(long taskId)
        {
            UserTaskId = taskId;
        }

        public long UserTaskId { get; }

        public class Response
        {
            public Response(Guid QuestionId, long userTaskId, DateTime askedOn, string askedBy, string subject, string question)
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
                UserTaskId = userTaskId;
                AskedOn = askedOn;
                AskedBy = askedBy;
                Subject = subject;
                Question = question;
            }

            public Guid QuestionId { get; }
            public long UserTaskId { get; }
            public DateTime AskedOn { get; }
            public string AskedBy { get; }
            public string Subject { get; }
            public string Question { get; }
        }
    }
}
