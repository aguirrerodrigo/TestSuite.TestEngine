using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class TestCaseRepository_TestAddExecution
    {
        File file;
        IFileSystemRepository fileSystemRepository;
        TestCaseRepository testCaseRepository;

        public TestCaseRepository_TestAddExecution()
        {
            this.file = new File();
            this.fileSystemRepository = Mock.Of<IFileSystemRepository>(r => r.CreateFile(It.IsAny<string>(), It.IsAny<string>()) == this.file);
            this.testCaseRepository = new TestCaseRepository("Root", fileSystemRepository);
        }

        [TestMethod]
        public void Test_ShouldCreateFile()
        {
            // Arrange
            var testCaseExecution = new TestCaseExecution();
            testCaseExecution.Name = "TestCase01";
            testCaseExecution.Ended = DateTime.Now.AddDays(1);
            testCaseExecution.Started = DateTime.Now;
            testCaseExecution.Status = ExecutionStatus.Failed;
            testCaseExecution.Steps = new List<TestStep>()
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
        public void Test_ShouldSetCreatedDateTime()
        {
            // Arrange
            var testCaseExecution = new TestCaseExecution();
            testCaseExecution.Name = "TestCaseExecution01";
            file.CreatedDateTime = DateTime.Now;

            // Act
            testCaseRepository.AddExecution("testCaseName", testCaseExecution);

            // Assert
            testCaseExecution.CreatedDateTime.ShouldEqual(file.CreatedDateTime);
        }
    }
}
