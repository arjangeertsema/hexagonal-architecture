using System;
using Common.CQRS.Abstractions.Queries;
using Common.UserTasks.Abstractions;

namespace Domain.Abstractions.UseCases
{
    public class GetReviewAnswerTaskUseCase : IQuery<GetReviewAnswerTaskUseCase.Response>, IUserTaskId
    {
        public GetReviewAnswerTaskUseCase(string userTaskId)
        {
            UserTaskId = userTaskId;
        }

        public string UserTaskId { get; }

        public class Response
        {
            public Guid QuestionId { get; }

            public Response(Guid questionId, string userTaskId, DateTime askedOn, string askedBy, string subject, string question, string answer)
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

                QuestionId = questionId;
                UserTaskId = userTaskId;
                AskedOn = askedOn;
                AskedBy = askedBy;
                Subject = subject;
                Question = question;
                Answer = answer;
            }

            public string UserTaskId { get; }
            public DateTime AskedOn { get; }
            public string Subject { get; }
            public string Question { get; }
            public string AskedBy { get; }
            public string Answer { get; }
        }
    }
}
