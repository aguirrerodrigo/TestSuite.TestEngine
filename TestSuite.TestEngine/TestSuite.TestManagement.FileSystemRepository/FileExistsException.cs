using System;

namespace TestSuite.TestManagement.FileSystemRepository
{
    internal class FileExistsException : Exception
    {
        public FileExistsException(string message) : base(message)
        {
        }
    }
}