using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepositoryTest_AddDefinition
    {
        private File createdFile;
        private IFileSystemRepository fileSystemRepository;
        private TestCaseRepository testCaseRepository;

        public TestCaseRepositoryTest_AddDefinition()
        {
            this.createdFile = new File();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(
                r => r.CreateFile(It.IsAny<string>(), It.IsAny<string>()) == this.createdFile);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void ShouldCreateTxtFile()
        {
            // Arrange
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = "TestCase01";
            testCaseDefinition.Definition = "Definition01";

            // Act
            testCaseRepository.AddDefinition("testCaseName", testCaseDefinition);

            // Assert
            Mock.Get(fileSystemRepository)
                .Verify(r => r.CreateFile("Root\\testCaseName\\Definitions\\TestCase01.txt", "Definition01"), Times.Once());
        }

        [TestMethod]
        public void ShouldSetCreatedDateTime()
        {
            // Arrange
            createdFile.CreatedDateTime = DateTime.Now;
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = "TestCaseDefinition01";

            // Act
            testCaseRepository.AddDefinition("testCaseName", testCaseDefinition);

            // Assert
            testCaseDefinition.CreatedDateTime.ShouldEqual(createdFile.CreatedDateTime);
        }
    }
}