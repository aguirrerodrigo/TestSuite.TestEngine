using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCase_TestSave
    {
        private ITestCaseRepository testCaseRepo;
        private TestCase testCase = new TestCase();

        public TestCase_TestSave()
        {
            testCaseRepo = Mock.Of<ITestCaseRepository>();
            Domain.Factories.Repository = Mock.Of<IRepositoryFactory>(
                f => f.CreateTestCaseRepository() == testCaseRepo);
        }

        [TestMethod]
        public void Test_ShouldSaveTestCaseInRepository()
        {
            // Arrange

            // Act
            testCase.Save();

            // Assert
            Mock.Get(testCaseRepo)
                .Verify(r => r.Save(testCase), Times.Once());
        }
    }
}
