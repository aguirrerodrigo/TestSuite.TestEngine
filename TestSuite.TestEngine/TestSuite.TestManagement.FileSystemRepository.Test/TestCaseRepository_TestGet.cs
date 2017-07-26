using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepository_TestGet
    {
        private Directory directory;
        private IFileSystemRepository fileSystemRepository;
        private TestCaseRepository testCaseRepository;

        public TestCaseRepository_TestGet()
        {
            this.directory = new Directory();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(r => r.GetDirectory(It.IsAny<string>()) == directory);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void Test_ShouldMapDirectoryToTestCase()
        {
            // Arrange
            directory.Name = "TestCase01";
            directory.CreatedDateTime = DateTime.Now.AddDays(-1);

            // Act
            var testCase = testCaseRepository.Get("TestCase01");

            // Assert
            Mock.Get(fileSystemRepository)
                .Verify(r => r.GetDirectory("Root\\TestCase01"), Times.Once());
            testCase.ShouldNotBeNull();
            testCase.Name.ShouldEqual("TestCase01");
            testCase.CreatedDateTime.ShouldEqual(directory.CreatedDateTime);
        }

        [TestMethod]
        public void Test_ShouldMapDefinitionFile()
        {
            // Arrange
            var file = NewFile("file1.txt", "contents123456", DateTime.Now.AddDays(-2));
            var files = new File[] { file };
            Mock.Get(fileSystemRepository)
                .Setup(r => r.FetchAllFiles("Root\\TestCase01\\Definitions"))
                .Returns(files);

            // Act
            var testCase = testCaseRepository.Get("TestCase01");
            var definition = testCase.Definitions.First();

            // Assert
            definition.Name.ShouldEqual("file1");
            definition.Definition.ShouldEqual("contents123456");
            definition.CreatedDateTime.ShouldEqual(file.CreatedDateTime);
        }

        private File NewFile(string fileName, string contents, DateTime createdDateTime)
        {
            var result = new File();
            result.Name = fileName;
            result.Contents = contents;
            result.CreatedDateTime = createdDateTime;

            return result;
        }
    }
}
