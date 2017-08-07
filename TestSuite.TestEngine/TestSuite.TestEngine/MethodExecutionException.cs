using System;
using System.Runtime.Serialization;

namespace TestSuite.TestEngine
{
    [Serializable]
    public class MethodExecutionException : Exception
    {
        public MethodExecutionException(string message) : base(message) { }
        protected MethodExecutionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}