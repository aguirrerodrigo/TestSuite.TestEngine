using System;
using System.Runtime.Serialization;

namespace TestSuite.TestEngine
{
    [Serializable]
    public class TestEngineConfigurationException : Exception
    {
        public TestEngineConfigurationException()
        {
        }

        public TestEngineConfigurationException(string message) : base(message)
        {
        }

        public TestEngineConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestEngineConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}