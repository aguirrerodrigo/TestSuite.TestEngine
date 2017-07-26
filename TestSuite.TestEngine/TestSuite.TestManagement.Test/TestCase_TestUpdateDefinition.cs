﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using System.Linq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCase_TestAddDefinition
    {
        private ITestCaseRepository testCaseRepository = Mock.Of<ITestCaseRepository>();
        private TestCase testCase = new TestCase();

        [TestMethod]
        public void Test_ShouldAddDefinitionToRepository()
        {
            // Arrange
            testCase.Name = "TestCaseName";

            // Act
            testCase.AddDefinition("TestDefinition", testCaseRepository);

            // Assert
            Mock.Get(testCaseRepository)
                .Verify(r => r.AddDefinition(
                    It.Is<string>(tc => tc == "TestCaseName"),
                    It.Is<TestCaseDefinition>(tcd => tcd.Definition == "TestDefinition")), Times.Once());
        }

        [TestMethod]
        public void Test_ShouldAutoSetName()
        {
            // Arrange
            testCase.Name = "TestCaseName";

            // Act
            testCase.AddDefinition("TestDefinition", testCaseRepository);

            // Assert
            testCase.Definitions.First().Name.ShouldNotBeNull();
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
            var testCaseDefinition = testCase.AddDefinition("TestDefinition", testCaseRepository);

            // Assert
            var result = testCase.Definitions.FirstOrDefault();
            result.ShouldEqual(testCaseDefinition);
        }
    }
}
