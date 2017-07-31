using System;

namespace TestSuite.TestManagement.FileSystemRepository
{
    public class DirectoryExistsException : Exception
    {
        public DirectoryExistsException(string message) : base(message)
        {
        }
    }
}