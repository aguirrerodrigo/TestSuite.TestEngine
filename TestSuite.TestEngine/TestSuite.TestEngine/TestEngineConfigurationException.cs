using System;

namespace TestSuite.TestEngine
{
    public class TestEngineConfigurationException : Exception
    {
        public TestEngineConfigurationException(string message) : base(message)
        {
        }

        public TestEngineConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}