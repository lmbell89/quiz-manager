using System;
using System.Runtime.Serialization;

namespace QuizManager.Core.Exceptions
{
    public class InvalidAnswerException : Exception
    {
        public InvalidAnswerException()
        {
        }

        public InvalidAnswerException(string message) : base(message)
        {
        }

        public InvalidAnswerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAnswerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
