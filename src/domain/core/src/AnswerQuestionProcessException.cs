using System;
using System.Runtime.Serialization;

namespace example.domain.core
{
    [Serializable]
    public class AnswerQuestionProcessException : Exception
    {
        public AnswerQuestionProcessException(string message) 
          : base(message) { }
        public AnswerQuestionProcessException(string message, Exception inner) 
          : base(message, inner) { }
        protected AnswerQuestionProcessException(SerializationInfo info, StreamingContext context)
          : base(info, context) { }
    }
}