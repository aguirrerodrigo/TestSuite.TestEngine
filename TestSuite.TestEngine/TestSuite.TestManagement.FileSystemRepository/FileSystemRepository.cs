using System;
using System.Collections.Generic;
using System.IO;
using IoDirectory = System.IO.Directory;

namespace TestSuite.TestManagement.FileSystemRepository
{
    internal class FileSystemRepository : IFileSystemRepository
    {
        private string path;

        public FileSystemRepository(string path)
        {
            this.path = path;
        }

        public Directory CreateDirectory(string name)
        {
            var dirPath = Path.Combine(this.path, name);
            if (IoDirectory.Exists(dirPath))
                throw new DirectoryExistsException($"Directory '{name}' already exists.");
            var dirInfo = IoDirectory.CreateDirectory(dirPath);

            var result = new Directory();
            result.CreatedDateTime = dirInfo.CreationTime;
            result.Name = name;
            result.Path = dirPath;

            return result;
        }

        public IEnumerable<Directory> FetchAll()
        {
            List<Directory> result = new List<Directory>();

            if (IoDirectory.Exists(this.path))
            {
                var dirs = IoDirectory.GetDirectories(this.path);
                foreach (var dir in dirs)
                {
                    var dirInfo = new DirectoryInfo(dir);
                    var directory = new Directory();
                    directory.CreatedDateTime = dirInfo.CreationTime;
                    directory.Name = dirInfo.Name;
                    directory.Path = dirInfo.FullName;
                    result.Add(directory);
                }
            }

            return result;
        }
    }
}
