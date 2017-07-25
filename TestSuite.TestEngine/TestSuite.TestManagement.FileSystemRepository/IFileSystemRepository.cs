using System.Collections.Generic;

namespace TestSuite.TestManagement.FileSystemRepository
{
    public interface IFileSystemRepository
    {
        Directory CreateDirectory(string directoryPath);
        IEnumerable<Directory> FetchAllDirectories(string path);
        Directory GetDirectory(string directoryPath);

        File CreateFile(string filePath, string contents);       
        IEnumerable<File> FetchAllFiles(string path);
    }
}