using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineProxyTest_SetClass
    {
        private ITestEngine testEngine = new TestEngineProxy();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }
        
        [TestMethod]
        public void SetClass_ShouldCreateMethodExecutionInstance()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");

            // Assert
            var methodExecution = testEngine.MethodExecution as MethodExecution;
            methodExecution.ShouldNotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void SetClass_ShouldThrowException_WhenQualifiedNameIncorrect()
        {
            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class1");
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void SetClass_ShouldThrowException_WhenAssemblyNotFound()
        {
            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void SetClass_ShouldThrowException_WhenClassNotFound()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class2, TestSuite.TestEngine.Mock");
        }
    }
}
