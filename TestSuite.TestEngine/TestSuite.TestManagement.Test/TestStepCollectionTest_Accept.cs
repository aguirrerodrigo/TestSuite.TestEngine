using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestStepCollectionTest_Accept
    {
        TestStepCollection steps = new TestStepCollection();

        [TestMethod]
        public void ShouldExecuteAcceptOnAllChildren()
        {
            // Arrange
            var visitor = Mock.Of<ITestStepVisitor>();
            var step1 = Mock.Of<TestStep>();
            var step2 = Mock.Of<TestStep>();
            steps.AddRange(new TestStep[] { step1, step2 });

            // Act
            steps.Accept(visitor);

            // Assert
            Mock.Get(step1)
                .Verify(s => s.Accept(visitor));
            Mock.Get(step2)
                .Verify(s => s.Accept(visitor));
        }
    }
}
