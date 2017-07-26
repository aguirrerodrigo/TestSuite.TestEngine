using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepository_TestAddDefinition
    {
        File file;
        IFileSystemRepository fileSystemRepository;
        TestCaseRepository testCaseRepository;

        public TestCaseRepository_TestAddDefinition()
        {
            this.file = new File();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(r => r.CreateFile(It.IsAny<string>(), It.IsAny<string>()) == this.file);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void Test_ShouldCreateFile()
        {
            // Arrange
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = "TestCase01";
            testCaseDefinition.Definition = "Definition";

            // Act
            testCaseRepository.AddDefinition("testCaseName", testCaseDefinition);

            // Assert
            Mock.Get(fileSystemRepository)
                .Verify(r => r.CreateFile("Root\\testCaseName\\Definitions\\TestCase01.txt", testCaseDefinition.Definition), Times.Once());
        }

        [TestMethod]
        public void Test_ShouldSetCreatedDateTime()
        {
            // Arrange
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = "TestCaseDefinition01";
            file.CreatedDateTime = DateTime.Now;

            // Act
            testCaseRepository.AddDefinition("testCaseName", testCaseDefinition);

            // Assert
            testCaseDefinition.CreatedDateTime.ShouldEqual(file.CreatedDateTime);
        }
    }
}
