using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using System;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepositoryTest_Create
    {
        private Directory createdDirectory;
        private IFileSystemRepository fileSystemRepository;
        private TestCaseRepository testCaseRepository;

        public TestCaseRepositoryTest_Create()
        {
            this.createdDirectory = new Directory();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(
                r => r.CreateDirectory(It.IsAny<string>()) == createdDirectory);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void ShouldCreateDirectory()
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
        public void ShouldSetCreatedDateTime()
        {
            // Arrange
            createdDirectory.CreatedDateTime = DateTime.Now;
            var testCase = new TestCase();
            testCase.Name = "TestCase01";
            
            // Act
            testCaseRepository.Create(testCase);

            // Assert
            testCase.CreatedDateTime.ShouldEqual(createdDirectory.CreatedDateTime);
        }
    }
}
