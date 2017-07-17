using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSuite.TestManagement.Web.Controllers;
using System.Web.Mvc;
using Moq;
using Should;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Web.Tests.Controllers
{
    [TestClass]
    public class TestCaseControllerTest
    {
        private ITestCaseRepository testCaseRepository;
        private TestCaseController controller;

        public TestCaseControllerTest()
        {
            this.testCaseRepository = Mock.Of<ITestCaseRepository>();
            this.controller = new TestCaseController(testCaseRepository);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            var testCaseName = "SampleTestCase";

            // Act
            var result = controller.Create(testCaseName) as ActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ShouldPersistToRepository()
        {
            // Arrange
            var testCaseName = "SampleTestCase";

            // Act
            var result = controller.Create(testCaseName) as ActionResult;

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.Create(
                    It.Is<TestCase>(tc => tc.Name == "SampleTestCase")), Times.Once());
        }
    }
}
