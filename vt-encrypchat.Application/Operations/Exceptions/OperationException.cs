using System;
using System.Runtime.Serialization;

namespace vt_encrypchat.Application.Operations.Exceptions
{
    public class OperationException: Exception
    {
        public OperationException()
        {
        }

        protected OperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public OperationException(string message) : base(message)
        {
        }

        public OperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}