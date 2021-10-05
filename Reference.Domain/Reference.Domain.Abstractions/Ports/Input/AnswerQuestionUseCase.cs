﻿using System;

namespace Reference.Domain.Abstractions.Ports.Input
{
    public class AnswerQuestionUseCase : IInputPort, IUserTask
    {
        public AnswerQuestionUseCase(Guid commandId, Guid questionId, long userTaskId, string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            CommandId = commandId;
            QuestionId = questionId;
            UserTaskId = userTaskId;
            Answer = answer;
        }

        public Guid CommandId { get; }
        public Guid QuestionId { get; }
        public long UserTaskId { get; set; }
        public string Answer { get; set; }
    }
}