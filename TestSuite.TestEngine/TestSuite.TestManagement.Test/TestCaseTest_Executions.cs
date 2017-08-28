using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCaseTest_Executions
    {
        private TestCase testCase = new TestCase();

        [TestMethod]
        public void ShouldSortByCreatedDateTime()
        {
            // Arrange
            var execution1 = NewExecution(created: DateTime.Today.AddDays(-3));
            var execution2 = NewExecution(created: DateTime.Today.AddDays(-2));
            var execution3 = NewExecution(created: DateTime.Today.AddDays(-1));
            var executions = new TestCaseExecution[]
            {
               execution1,
               execution2,
               execution3
            };

            // Act
            testCase.Executions = executions;

            // Assert
            var result = testCase.Executions.ToList();
            result[0].ShouldEqual(execution3);
            result[1].ShouldEqual(execution2);
            result[2].ShouldEqual(execution1);
        }

        private TestCaseExecution NewExecution(DateTime created)
        {
            var result = new TestCaseExecution();
            result.CreatedDateTime = created;
            return result;
        }
    }
}
