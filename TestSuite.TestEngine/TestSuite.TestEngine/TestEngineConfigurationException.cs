using System;
using System.Runtime.Serialization;

namespace TestSuite.TestEngine
{
    [Serializable]
    public class TestEngineConfigurationException : Exception
    {
        public TestEngineConfigurationException(string message) : base(message) { }
        protected TestEngineConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}