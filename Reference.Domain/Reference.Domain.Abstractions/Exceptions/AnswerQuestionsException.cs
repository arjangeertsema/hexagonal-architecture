using System;

namespace Reference.Domain.Abstractions.Exceptions
{
    public class AnswerQuestionsException : Exception
    {
        public AnswerQuestionsException(string message) 
          : base(message) { }
    }
}