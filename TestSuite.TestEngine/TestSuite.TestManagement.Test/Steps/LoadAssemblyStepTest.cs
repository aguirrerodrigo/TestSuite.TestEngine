using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestSuite.TestManagement.Test.Steps
{
    [TestClass]
    public class LoadAssemblyStepTest
    {
        private LoadAssemblyStep loadAssemblyStep = new LoadAssemblyStep();

        [TestMethod]
        public void Accept_ShouldBeVisited()
        {
            // Arrange
            var visitor = Mock.Of<ITestStepVisitor>();

            // Act
            loadAssemblyStep.Accept(visitor);

            // Assert
            Mock.Get(visitor)
                .Verify(v => v.Visit(loadAssemblyStep));
        }
    }
}
