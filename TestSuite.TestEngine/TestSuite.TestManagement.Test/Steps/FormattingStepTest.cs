using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestSuite.TestManagement.Test.Steps
{
    [TestClass]
    public class FormattingStepTest
    {
        private FormattingStep formattingStep = new FormattingStep();

        [TestMethod]
        public void Accept_ShouldBeVisited()
        {
            // Arrange
            var visitor = Mock.Of<ITestStepVisitor>();

            // Act
            formattingStep.Accept(visitor);

            // Assert
            Mock.Get(visitor)
                .Verify(v => v.Visit(formattingStep));
        }
    }
}
