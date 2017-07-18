using System.Collections.Generic;

namespace TestSuite.TestManagement.FileSystemRepository
{
    public interface IFileSystemRepository
    {
        Directory CreateDirectory(string name);
        IEnumerable<Directory> FetchAll();
    }
}