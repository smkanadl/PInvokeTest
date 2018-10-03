using System;
using System.Runtime.Serialization;

namespace ManagedModel.Sle
{
    public class InvalidMatrixOperationException : Exception
    {
        public InvalidMatrixOperationException()
        {
        }

        public InvalidMatrixOperationException(string message)
            : base(message)
        {
        }

        public InvalidMatrixOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidMatrixOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
