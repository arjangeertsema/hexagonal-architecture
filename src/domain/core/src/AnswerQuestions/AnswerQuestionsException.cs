using System;
using System.Runtime.Serialization;

namespace Reference.Domain.Core.AnswerQuestions
{
    [Serializable]
    public class AnswerQuestionsException : Exception
    {
        public AnswerQuestionsException(string message) 
          : base(message) { }
          
        public AnswerQuestionsException(string message, Exception inner) 
          : base(message, inner) { }

        protected AnswerQuestionsException(SerializationInfo info, StreamingContext context)
          : base(info, context) { }
    }
}