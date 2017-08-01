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
            var result = controller.Index("testCase") as ActionResult;

            // Assert
            result.ShouldNotBeNull();
        }

        [TestMethod]
        public void GetDefinition()
        {
            // Arrange

            // Act
            var result = controller.GetDefinition("testCase") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDefinition_ShouldAutoSelectDefinition_WhenDefinitionNameIsNull()
        {
            // Arrange
            var definitions = new List<TestCaseDefinition>();
            definitions.Add(new TestCaseDefinition() { Name = "definition1" });
            definitions.Add(new TestCaseDefinition() { Name = "definition2" });
            testCase.Definitions = definitions;

            // Act
            controller.GetDefinition("testCase", null);
            var model = controller.ViewData.Model as TestCaseViewModel;

            // Assert
            model.Definitions.Any(d => d.IsSelected).ShouldBeTrue();
        }

        [TestMethod]
        public void GetDefinition_ShouldSelectDefinition()
        {
            // Arrange
            var definitions = new List<TestCaseDefinition>();
            definitions.Add(new TestCaseDefinition() { Name = "definition1" });
            definitions.Add(new TestCaseDefinition() { Name = "definition2" });
            testCase.Definitions = definitions;

            // Act
            controller.GetDefinition("testCase", "definition2");
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

        [TestMethod]
        public void UpdateDefinition_ShouldAddNewExecution()
        {
            // Arrange
            string testCaseName = "testCase";
            string definition = "definition";

            // Act
            controller.UpdateDefinition(testCaseName, definition);

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.AddExecution(testCaseName, It.IsAny<TestCaseExecution>()));
        }

        [TestMethod]
        public void GetResult_ShouldReturnView_WhenThereAreExecutions()
        {
            // Arrange
            var executions = new List<TestCaseExecution>();
            executions.Add(new TestCaseExecution());
            testCase.Executions = executions;

            // Act
            var result = controller.GetResult("testCase") as ViewResult;

            // Assert
            result.ShouldNotBeNull();
        }

        [TestMethod]
        public void GetResult_ShouldRedirectToDefinition_WhenNoExecutions()
        {
            // Arrange

            // Act
            var result = controller.GetResult("testCase") as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].ShouldEqual("GetDefinition");
        }

        [TestMethod]
        public void GetResult_ShouldAutoSelectResult_WhenResultNameIsNull()
        {
            // Arrange
            var executions = new List<TestCaseExecution>();
            executions.Add(new TestCaseExecution() { Name = "result1" });
            executions.Add(new TestCaseExecution() { Name = "result2" });
            testCase.Executions = executions;

            // Act
            controller.GetResult("testCase", null);
            var model = controller.ViewData.Model as TestCaseViewModel;

            // Assert
            model.Results.Any(d => d.IsSelected).ShouldBeTrue();
        }

        [TestMethod]
        public void GetResult_ShouldSelectResult()
        {
            // Arrange
            var executions = new List<TestCaseExecution>();
            executions.Add(new TestCaseExecution() { Name = "result1" });
            executions.Add(new TestCaseExecution() { Name = "result2" });
            testCase.Executions = executions;

            // Act
            controller.GetResult("testCase", "result2");
            var model = controller.ViewData.Model as TestCaseViewModel;

            // Assert
            model.Results.First(d => d.Name == "result2").IsSelected.ShouldBeTrue();
        }

        [TestMethod]
        public void RunTest_ShuldUpdateExecution()
        {
            // Arrange
            var testRunner = Mock.Of<ITestRunner>();
            var executions = new List<TestCaseExecution>();
            executions.Add(new TestCaseExecution() { Name = "result1" });
            testCase.Executions = executions;

            // Act
            controller.RunTest("testCase", "result1", testRunner);

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.UpdateExecution("testCase", It.IsAny<TestCaseExecution>()), Times.Once());
        }
    }
}
