using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepositoryTest_FetchAll
    {
        private List<Directory> directories;
        private IFileSystemRepository fileSystemRepository;
        private TestCaseRepository testCaseRepository;

        public TestCaseRepositoryTest_FetchAll()
        {
            this.directories = new List<Directory>();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(r => r.FetchAllDirectories(It.IsAny<string>()) == directories);
            this.testCaseRepository = new TestCaseRepository(string.Empty, fileSystemRepository);
        }

        [TestMethod]
        public void ShouldRetrieveFromFileSystemRepository()
        {
            // Arrange
            var dir1 = NewDirectory(DateTime.Now, "dir1");
            directories.Add(dir1);

            // Act
            var result = this.testCaseRepository.FetchAll().ToList();

            // Assert
            result[0].Name.ShouldEqual("dir1");
            result[0].CreatedDateTime.ShouldEqual(dir1.CreatedDateTime);
        }

        [TestMethod]
        public void ShouldSortByName()
        {
            // Arrange
            var dir1 = NewDirectory(DateTime.Now.AddDays(-3), "dir1");
            var dir2 = NewDirectory(DateTime.Now.AddDays(-2), "dir2");
            var dir3 = NewDirectory(DateTime.Now.AddDays(-1), "dir3");
            directories.AddRange(new Directory[] { dir1, dir2, dir3 });

            // Act
            var result = this.testCaseRepository.FetchAll().ToList();

            // Assert
            result[0].Name.ShouldEqual("dir1");
            result[1].Name.ShouldEqual("dir2");
            result[2].Name.ShouldEqual("dir3");
        }

        private Directory NewDirectory(DateTime created, string name = null)
        {
            var result = new Directory();
            result.CreatedDateTime = created;
            result.Name = name;

            return result;
        }
    }
}
