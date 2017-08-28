using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using System.Linq;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCaseTest_Definitions
    {
        private TestCase testCase = new TestCase();

        [TestMethod]
        public void ShouldSortByCreatedDateTime()
        {
            // Arrange
            var definition1 = NewDefinition(created: DateTime.Today.AddDays(-3));
            var definition2 = NewDefinition(created: DateTime.Today.AddDays(-2));
            var definition3 = NewDefinition(created: DateTime.Today.AddDays(-1));
            var definitions = new TestCaseDefinition[]
            {
               definition1,
               definition2,
               definition3
            };

            // Act
            testCase.Definitions = definitions;

            // Assert
            var result = testCase.Definitions.ToList();
            result[0].ShouldEqual(definition3);
            result[1].ShouldEqual(definition2);
            result[2].ShouldEqual(definition1);
        }

        private TestCaseDefinition NewDefinition(DateTime created)
        {
            var result = new TestCaseDefinition();
            result.CreatedDateTime = created;
            return result;
        }
    }
}
