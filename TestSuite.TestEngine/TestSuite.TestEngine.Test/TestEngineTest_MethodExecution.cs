using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineTest_MethodExecution
    {
        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void ShouldThrowException_WhenClassNotSet()
        {
            // Arrange
            var testEngine = new TestEngine();

            // Act
            var methodExecution = testEngine.MethodExecution;
        }
    }
}
