using System;
using System.Runtime.Serialization;

namespace TestSuite.TestManagement
{
    [Serializable]
    internal class DomainFactoryException : Exception
    {
        public DomainFactoryException()
        {
        }

        public DomainFactoryException(string message) : base(message)
        {
        }

        public DomainFactoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainFactoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}