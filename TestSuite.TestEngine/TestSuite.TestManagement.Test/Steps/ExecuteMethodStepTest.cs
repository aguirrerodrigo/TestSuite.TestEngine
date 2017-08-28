using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class ExecuteMethodStepTest
    {
        private ExecuteMethodStep executeMethodStep = new ExecuteMethodStep();

        [TestMethod]
        public void Accept_ShouldBeVisited()
        {
            // Arrange
            var visitor = Mock.Of<ITestStepVisitor>();

            // Act
            executeMethodStep.Accept(visitor);

            // Assert
            Mock.Get(visitor)
                .Verify(v => v.Visit(executeMethodStep), Times.Once);
        }

        [TestMethod]
        public void GetFormattedMethodName_ShouldReplaceSpacesWithUnderscore()
        {
            // Arrange
            executeMethodStep.MethodName = "Method Name";

            // Act
            var formattedMethodName = executeMethodStep.GetFormattedMethodName();

            // Assert
            formattedMethodName.ShouldEqual("Method_Name");
        }

        [TestMethod]
        public void GetFormattedMethodName_ShouldReturnNull_WhenNameIsNull()
        {
            // Arrange
            executeMethodStep.MethodName = null;

            // Act
            var formattedMethodName = executeMethodStep.GetFormattedMethodName();

            // Assert
            formattedMethodName.ShouldBeNull();
        }
    }
}
