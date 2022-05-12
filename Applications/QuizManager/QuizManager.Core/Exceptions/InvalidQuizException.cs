using System;
using System.Runtime.Serialization;

namespace QuizManager.Core.Exceptions
{
    public class InvalidQuizException : Exception
    {
        public InvalidQuizException()
        {
        }

        public InvalidQuizException(string message) : base(message)
        {
        }

        public InvalidQuizException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidQuizException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
