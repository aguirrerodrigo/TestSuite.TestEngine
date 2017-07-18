using System;
using System.Runtime.Serialization;

namespace TestSuite.TestManagement.FileSystemRepository
{
    [Serializable]
    public class DirectoryExistsException : Exception
    {
        public DirectoryExistsException()
        {
        }

        public DirectoryExistsException(string message) : base(message)
        {
        }

        public DirectoryExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DirectoryExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}