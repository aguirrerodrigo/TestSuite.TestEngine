using System;
using System.Runtime.Serialization;

namespace TestSuite.TestEngine
{
    [Serializable]
    public class MethodExecutionException : Exception
    {
        public MethodExecutionException()
        {
        }

        public MethodExecutionException(string message) : base(message)
        {
        }

        public MethodExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MethodExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}