using System;

namespace TestSuite.TestEngine
{
    public class MethodExecutionException : Exception
    {
        public MethodExecutionException(string message) : base(message)
        {
        }
    }
}