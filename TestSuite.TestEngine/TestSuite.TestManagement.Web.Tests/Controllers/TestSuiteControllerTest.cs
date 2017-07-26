using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TestSuiteControllerTest
    {
        private List<TestCase> testCases;
        private ITestCaseRepository testCaseRepository;
        private TestSuiteController controller;

        public TestSuiteControllerTest()
        {
            this.testCases = new List<TestCase>();
            this.testCaseRepository = Mock.Of<ITestCaseRepository>(r => r.FetchAll() == testCases);
            this.controller = new TestSuiteController(testCaseRepository);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            var model = result.ViewData.Model as TestSuiteViewModel;
            model.ShouldNotBeNull();
        }

        [TestMethod]
        public void Index_ShouldSetModelFromTempData()
        {
            // Arrange
            var viewModel = new TestSuiteViewModel();
            controller.TempData["Model"] = viewModel;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            var model = result.ViewData.Model;
            model.ShouldEqual(viewModel);
        }

        [TestMethod]
        public void Index_ShouldAddErrorToModelState()
        {
            // Arrange
            controller.TempData["Error"] = "error";

            // Act
            controller.Index();

            // Assert
            controller.ModelState[string.Empty].Errors
                .Select(e => e.ErrorMessage)
                .ShouldContain("error");
        }

        [TestMethod]
        public void Index_ShouldRetrieveTestCases()
        {
            // Arrange
            testCases.Add(new TestCase());
            testCases.Add(new TestCase());

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as TestSuiteViewModel;

            // Assert
            model.TestCases.ShouldEqual(testCases);
        }

        [TestMethod]
        public void CreateTestCase_ShouldRedirectToIndex()
        {
            // Arrange
            var name = "SampleTestCase";

            // Act
            var result = controller.CreateTestCase(name) as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].ShouldEqual("Index");
            result.RouteValues["controller"].ShouldBeNull();
        }

        [TestMethod]
        public void CreateTestCase_ShouldPersistToRepository()
        {
            // Arrange
            var name = "SampleTestCase";

            // Act
            var result = controller.CreateTestCase(name) as ActionResult;

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.Create(
                    It.Is<TestCase>(tc => tc.Name == "SampleTestCase")), Times.Once());
        }

        [TestMethod]
        public void CreateTestCase_ShouldSetError_OnError()
        {
            // Arrange
            Mock.Get(testCaseRepository)
                .Setup(r => r.Create(It.IsAny<TestCase>()))
                .Throws<DivideByZeroException>();

            // Act
            controller.CreateTestCase("testCaseName");

            // Assert
            controller.TempData["Error"].ShouldNotBeNull();
            controller.TempData["Model"].ShouldNotBeNull();
        }
    }
}
