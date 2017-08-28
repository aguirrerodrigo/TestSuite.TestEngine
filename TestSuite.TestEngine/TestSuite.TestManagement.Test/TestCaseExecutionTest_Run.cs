using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCaseExecutionTest_Run
    {
        private ITestRunner testRunner = Mock.Of<ITestRunner>();
        private TestCaseExecution testCaseExecution = new TestCaseExecution();

        [TestMethod]
        public void ShouldExecuteUsingTestRunner()
        {
            // Act
            testCaseExecution.Run(testRunner);

            // Assert
            Mock.Get(testRunner).Verify(
                r => r.Run(testCaseExecution), Times.Once);
        }
    }
}
