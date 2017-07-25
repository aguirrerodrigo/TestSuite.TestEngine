using System;

namespace TestSuite.TestManagement.FileSystemRepository
{
    public class File
    {
        public DateTime CreatedDateTime { get; set; }
        public string Name { get; set; }
        public string Path { get;  set; }
        public string Contents { get; set; }
    }
}