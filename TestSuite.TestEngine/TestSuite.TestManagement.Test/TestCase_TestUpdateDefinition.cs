using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using System.Linq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCase_TestUpdateDefinition
    {
        private ITestCaseRepository testCaseRepo;
        private TestCase testCase = new TestCase();

        public TestCase_TestUpdateDefinition()
        {
            testCaseRepo = Mock.Of<ITestCaseRepository>();
            Domain.Factories.Repository = Mock.Of<IRepositoryFactory>(
                f => f.CreateTestCaseRepository() == testCaseRepo);
        }

        [TestMethod]
        public void Test_ShouldAddDefinitionToRepository()
        {
            // Arrange
            testCase.Name = "TestCaseName";

            // Act
            testCase.UpdateDefinition("TestDefinition");

            // Assert
            Mock.Get(testCaseRepo)
                .Verify(r => r.AddDefinition(
                    It.Is<string>(tc => tc == "TestCaseName"),
                    It.Is<TestCaseDefinition>(tcd => tcd.Definition == "TestDefinition")), Times.Once());
        }

        [TestMethod]
        public void Test_ShouldAddDefinitionAsFirstItem()
        {
            // Arrange
            testCase.Definitions = new TestCaseDefinition[]
            {
                new TestCaseDefinition(),
                new TestCaseDefinition(),
                new TestCaseDefinition()
            };

            // Act
            var testCaseDefinition = testCase.UpdateDefinition("TestDefinition");

            // Assert
            var result = testCase.Definitions.FirstOrDefault();
            result.ShouldEqual(testCaseDefinition);
        }

        [TestMethod]
        [Ignore]
        public void Test_ShouldCreateTestCaseExecution()
        {
        }
    }
}
