using System.Collections.Generic;
using System.IO;
using System.Text;
using IoDirectory = System.IO.Directory;
using IoFile = System.IO.File;

namespace TestSuite.TestManagement.FileSystemRepository
{
    internal class FileSystemRepository : IFileSystemRepository
    {
        public Directory CreateDirectory(string directoryPath)
        {
            if (IoDirectory.Exists(directoryPath))
                throw new DirectoryExistsException($"Directory '{directoryPath}' already exists.");
            var dirInfo = IoDirectory.CreateDirectory(directoryPath);
            var directory = MapDirectory(dirInfo);

            return directory;
        }

        private Directory MapDirectory(DirectoryInfo directoryInfo)
        {
            var directory = new Directory();
            directory.CreatedDateTime = directoryInfo.CreationTime;
            directory.Name = directoryInfo.Name;
            directory.Path = directoryInfo.FullName;

            return directory;
        }

        public IEnumerable<Directory> FetchAllDirectories(string path)
        {
            List<Directory> result = new List<Directory>();

            if (IoDirectory.Exists(path))
            {
                var dirs = IoDirectory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    var dirInfo = new DirectoryInfo(dir);
                    var directory = MapDirectory(dirInfo);
                    result.Add(directory);
                }
            }

            return result;
        }

        public Directory GetDirectory(string directoryPath)
        {
            var dirInfo = new DirectoryInfo(directoryPath);
            var directory = MapDirectory(dirInfo);

            return directory;
        }

        public File CreateFile(string filePath, string contents)
        {
            lock (filePath)
            {
                if (IoFile.Exists(filePath))
                    throw new FileExistsException($"File '{filePath}' already exists.");

                var dirPath = Path.GetDirectoryName(filePath);
                IoDirectory.CreateDirectory(dirPath);

                IoFile.WriteAllText(filePath, contents, Encoding.Unicode);
                var fileInfo = new FileInfo(filePath);

                var file = new File();
                file.CreatedDateTime = fileInfo.CreationTime;
                file.Name = fileInfo.Name;
                file.Path = fileInfo.FullName;
                file.Contents = contents;

                return file;
            }
        }

        public IEnumerable<File> FetchAllFiles(string path)
        {
            List<File> result = new List<File>();

            if(IoDirectory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);
                var fileInfos = dirInfo.GetFiles();
                foreach (var fileInfo in fileInfos)
                {
                    var file = MapFile(fileInfo);
                    result.Add(file);
                }
            }

            return result;
        }

        private File MapFile(FileInfo fileInfo)
        {
            var file = new File();
            file.Contents = IoFile.ReadAllText(fileInfo.FullName);
            file.CreatedDateTime = fileInfo.CreationTime;
            file.Name = fileInfo.Name;
            file.Path = fileInfo.FullName;

            return file;
        }
    }
}
