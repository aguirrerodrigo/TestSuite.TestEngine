using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestSuite.TestManagement.Test.Steps
{
    [TestClass]
    public class SetClassStepTest
    {
        private SetClassStep setClassStep = new SetClassStep();

        [TestMethod]
        public void Accept_ShouldBeVisited()
        {
            // Arrange
            var visitor = Mock.Of<ITestStepVisitor>();

            // Act
            setClassStep.Accept(visitor);

            // Assert
            Mock.Get(visitor)
                .Verify(v => v.Visit(setClassStep));
        }
    }
}
