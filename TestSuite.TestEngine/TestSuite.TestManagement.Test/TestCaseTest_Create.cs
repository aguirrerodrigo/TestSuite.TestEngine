using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCaseTest_Create
    {
        private ITestCaseRepository testCaseRepository = Mock.Of<ITestCaseRepository>();
        private TestCase testCase = new TestCase();

        [TestMethod]
        public void ShouldCreateTestCaseInRepository()
        {
            // Act
            testCase.Create(testCaseRepository);

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.Create(testCase), Times.Once());
        }
    }
}
