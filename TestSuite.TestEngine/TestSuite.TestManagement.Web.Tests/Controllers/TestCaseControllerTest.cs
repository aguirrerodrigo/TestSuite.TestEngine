using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.Controllers;
using Should;
using System.Linq;

namespace TestSuite.TestManagement.Web.Tests.Controllers
{
    [TestClass]
    public class TestCaseControllerTest
    {
        private TestCase testCase;
        private ITestCaseRepository testCaseRepository;
        private TestCaseController controller;

        public TestCaseControllerTest()
        {
            this.testCase = new TestCase();
            this.testCaseRepository = Mock.Of<ITestCaseRepository>(r => r.Get(It.IsAny<string>()) == testCase);
            this.controller = new TestCaseController(testCaseRepository);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange

            // Act
            var result = controller.Index("testCase", null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateDefinition()
        {
            // Arrange
            string testCaseName = "testCase";
            string definition = "definition";

            // Act
            var result = controller.UpdateDefinition(testCaseName, definition) as ActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateDefinition_ShouldAddNewDefinition()
        {
            // Arrange
            string testCaseName = "testCase";
            string definition = "definition";

            // Act
            controller.UpdateDefinition(testCaseName, definition);

            // Assert
            testCase.Definitions.Any(d => d.Definition == definition).ShouldBeTrue();
            Mock.Get(testCaseRepository)
                .Verify(r => r.AddDefinition(It.IsAny<string>(), It.Is<TestCaseDefinition>(d => d.Definition == definition)));
        }
    }
}
