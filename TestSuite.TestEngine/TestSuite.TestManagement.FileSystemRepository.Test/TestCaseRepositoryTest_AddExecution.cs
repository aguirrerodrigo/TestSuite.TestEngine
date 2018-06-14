using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepositoryTest_AddExecution
    {
        private File createdFile;
        private IFileSystemRepository fileSystemRepository;
        private TestCaseRepository testCaseRepository;

        public TestCaseRepositoryTest_AddExecution()
        {
            this.createdFile = new File();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(
                r => r.CreateFile(It.IsAny<string>(), It.IsAny<string>()) == this.createdFile);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void ShouldCreateXmlFile()
        {
            // Arrange
            var testCaseExecution = new TestCaseExecution();
            testCaseExecution.Name = "TestCase01";
            testCaseExecution.Ended = DateTime.Now.AddDays(1);
            testCaseExecution.Started = DateTime.Now;
            testCaseExecution.Status = ExecutionStatus.Failed;
            testCaseExecution.Steps = new TestStepCollection()
            {
                new LoadAssemblyStep(),
                new SetClassStep(),
                new FormattingStep(),
                new ExecuteMethodStep(),
                new ExecuteMethodStep(),
                new FormattingStep(),
                new ExecuteMethodStep()
            };
            var xml = testCaseExecution.ToXml();

            // Act
            testCaseRepository.AddExecution("testCaseName", testCaseExecution);

            // Assert
            Mock.Get(fileSystemRepository)
                .Verify(r => r.CreateFile("Root\\testCaseName\\Executions\\TestCase01.xml", xml), Times.Once());
        }

        [TestMethod]
        public void ShouldSetCreatedDateTime()
        {
            // Arrange
            createdFile.CreatedDateTime = DateTime.Now;
            var testCaseExecution = new TestCaseExecution();
            testCaseExecution.Name = "TestCaseExecution01";
            
            // Act
            testCaseRepository.AddExecution("testCaseName", testCaseExecution);

            // Assert
            testCaseExecution.CreatedDateTime.ShouldEqual(createdFile.CreatedDateTime);
        }
    }
}
