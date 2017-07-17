using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCase_TestSave
    {
        private ITestCaseRepository testCaseRepository = Mock.Of<ITestCaseRepository>();
        private TestCase testCase = new TestCase();

        [TestMethod]
        public void Test_ShouldSaveTestCaseInRepository()
        {
            // Arrange

            // Act
            testCase.Create(testCaseRepository);

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.Create(testCase), Times.Once());
        }
    }
}
