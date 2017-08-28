using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using System.Linq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCaseTest_AddExecution
    {
        private ITestStepFactory testStepFactory = Mock.Of<ITestStepFactory>();
        private ITestCaseRepository testCaseRepository = Mock.Of<ITestCaseRepository>();
        private TestCase testCase = new TestCase();

        [TestMethod]
        public void ShouldAddExecutionToRepository()
        {
            // Arrange
            testCase.Name = "TestCaseName";

            // Act
            testCase.AddExecution("TestDefinition", testCaseRepository, testStepFactory);

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.AddExecution(
                    It.Is<string>(tc => tc == "TestCaseName"),
                    It.IsAny<TestCaseExecution>()), Times.Once());
        }

        [TestMethod]
        public void ShouldAutoSetName()
        {
            // Arrange
            testCase.Name = "TestCaseName";

            // Act
            testCase.AddExecution("TestDefinition", testCaseRepository, testStepFactory);

            // Assert
            testCase.Executions.First().Name.ShouldNotBeNull();
        }

        [TestMethod]
        public void ShouldAddExecutionAsFirstItem()
        {
            // Arrange
            testCase.Executions = new TestCaseExecution[]
            {
                new TestCaseExecution(),
                new TestCaseExecution(),
                new TestCaseExecution()
            };

            // Act
            var testCaseExecution = testCase.AddExecution("TestDefinition", testCaseRepository, testStepFactory);

            // Assert
            var result = testCase.Executions.First();
            result.ShouldEqual(testCaseExecution);
        }

        [TestMethod]
        public void ShouldCreateTestStepsPerLineOfDefinition()
        {
            // Arrange
            var definition = "step\r\nstep\nstep\rstep";
            Mock.Get(testStepFactory)
                .Setup(f => f.Create(It.IsAny<string>()))
                .Returns(Mock.Of<TestStep>());

            // Act
            var testCaseExecution = testCase.AddExecution(definition, testCaseRepository, testStepFactory);

            // Assert
            Mock.Get(testStepFactory)
                .Verify(f => f.Create(It.IsAny<string>()), Times.Exactly(4));
            Mock.Get(testStepFactory)
                .Verify(f => f.Create("step"), Times.Exactly(4));
            testCaseExecution.Steps.Count().ShouldEqual(4);
        }
    }
}
