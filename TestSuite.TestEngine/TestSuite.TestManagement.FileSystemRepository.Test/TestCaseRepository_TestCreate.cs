using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using System;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepository_TestCreate
    {
        Directory directory;
        IFileSystemRepository fileSystemRepository;
        TestCaseRepository testCaseRepository;

        public TestCaseRepository_TestCreate()
        {
            this.directory = new Directory();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(r => r.CreateDirectory(It.IsAny<string>()) == this.directory);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void Test_ShouldCreateDirectory()
        {
            // Arrange
            var testCase = new TestCase();
            testCase.Name = "TestCase01";

            // Act
            testCaseRepository.Create(testCase);

            // Assert
            Mock.Get(fileSystemRepository)
                .Verify(r => r.CreateDirectory("Root\\TestCase01"), Times.Once());
        }

        [TestMethod]
        public void Test_ShouldSetCreatedDateTime()
        {
            // Arrange
            var testCase = new TestCase();
            testCase.Name = "TestCase01";
            directory.CreatedDateTime = DateTime.Now;

            // Act
            testCaseRepository.Create(testCase);

            // Assert
            testCase.CreatedDateTime.ShouldEqual(directory.CreatedDateTime);
        }
    }
}
