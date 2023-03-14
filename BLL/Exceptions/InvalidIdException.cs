using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException(string message)
            : base(message)
        {
        }

        public InvalidIdException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}