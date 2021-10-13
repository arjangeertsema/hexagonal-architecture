using System;
using Common.CQRS.Abstractions;
using Common.CQRS.Abstractions.Queries;

namespace Domain.Abstractions.Ports.Input
{
    public class GetModifyAnswerTaskUseCase : IQuery<GetModifyAnswerTaskUseCase.Response>, IUserTask
    {
        public GetModifyAnswerTaskUseCase(long userTaskId)
        {
            this.UserTaskId = userTaskId;
        }

        public long UserTaskId { get; }

        public class Response
        {
            public Guid QuestionId { get; }

            public Response(Guid questionId, long userTaskId, DateTime askedOn, string askedBy, string subject, string question, string answer, string rejection)
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

                if (string.IsNullOrEmpty(answer))
                {
                    throw new ArgumentException($"'{nameof(answer)}' cannot be null or empty.", nameof(answer));
                }

                if (string.IsNullOrEmpty(rejection))
                {
                    throw new ArgumentException($"'{nameof(rejection)}' cannot be null or empty.", nameof(rejection));
                }

                QuestionId = questionId;
                UserTaskId = userTaskId;
                AskedOn = askedOn;
                AskedBy = askedBy;
                Subject = subject;
                Question = question;
                Answer = answer;
                Rejection = rejection;
            }

            public long UserTaskId { get; }
            public DateTime AskedOn { get; }
            public string Subject { get; }
            public string Question { get; }
            public string AskedBy { get; }
            public string Answer { get; }
            public string Rejection { get; }
        }
    }
}
