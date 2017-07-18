using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.Controllers;
using TestSuite.TestManagement.Web.ViewModels;

namespace TestSuite.TestManagement.Web.Tests.Controllers
{
    [TestClass]
    public class TestCaseControllerTest
    {
        private List<TestCase> testCases;
        private ITestCaseRepository testCaseRepository;
        private TestCaseController controller;

        public TestCaseControllerTest()
        {
            this.testCases = new List<TestCase>();
            this.testCaseRepository = Mock.Of<ITestCaseRepository>(r => r.FetchAll() == testCases);
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
        public void Index_ShouldRetrieveTestCases()
        {
            // Arrange
            testCases.Add(new TestCase());
            testCases.Add(new TestCase());

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as TestCaseViewModel;

            // Assert
            model.TestCases.ShouldEqual(testCases);
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
