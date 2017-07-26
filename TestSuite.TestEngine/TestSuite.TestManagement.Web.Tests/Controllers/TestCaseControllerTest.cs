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
        public void Index_ShouldAutoSelectDefinition_WhenDefinitionNameIsNull()
        {
            // Arrange
            var definitions = new List<TestCaseDefinition>();
            definitions.Add(new TestCaseDefinition() { Name = "definition1" });
            definitions.Add(new TestCaseDefinition() { Name = "definition2" });
            testCase.Definitions = definitions;

            // Act
            controller.Index("testCase", null);
            var model = controller.ViewData.Model as TestCaseViewModel;

            // Assert
            model.Definitions.Any(d => d.IsSelected).ShouldBeTrue();
        }

        [TestMethod]
        public void Index_ShouldSelectDefinition()
        {
            // Arrange
            var definitions = new List<TestCaseDefinition>();
            definitions.Add(new TestCaseDefinition() { Name = "definition1" });
            definitions.Add(new TestCaseDefinition() { Name = "definition2" });
            testCase.Definitions = definitions;

            // Act
            controller.Index("testCase", "definition2");
            var model = controller.ViewData.Model as TestCaseViewModel;

            // Assert
            model.Definitions.First(d => d.Name == "definition2").IsSelected.ShouldBeTrue();
        }

        [TestMethod]
        public void UpdateDefinition()
        {
            // Arrange
            string testCaseName = "testCase";
            string definition = "definition";

            // Act
            var result = controller.UpdateDefinition(testCaseName, definition) as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].ShouldEqual("Index");
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
            Mock.Get(testCaseRepository)
                .Verify(r => r.AddDefinition(testCaseName, It.Is<TestCaseDefinition>(d => d.Definition == definition)));
        }
    }
}
