using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngine_TestSetClass
    {
        public ITestEngine testEngine = new TestEngineProxy();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void Test()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");

            // Assert
            testEngine.MethodExecution.ShouldNotBeNull();
        }

        [TestMethod]
        public void TestClassWithReferenceAutoLoad()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
            // testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.MockReference.dll");
            // no need to loadd assembly, assembly will be loaded since it is on the same directory.
            testEngine.SetClass("TestSuite.TestEngine.Mock.ClassWithReference, TestSuite.TestEngine.Mock");

            // Act
            testEngine.MethodExecution.Execute("Method1");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void TestAssemblyNotLoaded_ThrowsException()
        {
            // Arrange

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.Class, TestSuite.TestEngine.Mock");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void TestClassNotFound_ThrowsException()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.Class, TestSuite.TestEngine.Mock");

            // Assert
        }
    }
}
