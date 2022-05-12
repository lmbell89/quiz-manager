using System;
using System.Runtime.Serialization;

namespace QuizManager.Core.Exceptions
{
    public class InvalidQuestionException : Exception
    {
        public InvalidQuestionException()
        {
        }

        public InvalidQuestionException(string message) : base(message)
        {
        }

        public InvalidQuestionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidQuestionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
