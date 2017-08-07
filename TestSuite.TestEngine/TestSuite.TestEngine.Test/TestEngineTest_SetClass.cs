using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineTest_SetClass
    {
        private TestEngine testEngine = new TestEngine();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void ShouldCreateTestInstance()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");

            // Assert
            var testInstance = testEngine.TestInstance;
            var typeFullName = testInstance.GetType().FullName;
            typeFullName.ShouldEqual("TestSuite.TestEngine.Mock.Class1");
        }

        [TestMethod]
        public void ShouldAutoLoadReferencedAssemblyToAppDomain_WhenClassReferencesAssembly()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.ClassWithReference, TestSuite.TestEngine.Mock");

            // Assert
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "TestSuite.TestEngine.MockReference");
            assembly.ShouldNotBeNull();
        }

        [TestMethod]
        public void ShouldCreateMethodExecutionInstance()
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
        public void ShouldThrowException_WhenQualifiedNameIncorrect()
        {
            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class1");
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void ShouldThrowException_WhenAssemblyNotFound()
        {
            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void ShouldThrowException_WhenClassNotFound()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass(@"TestSuite.TestEngine.Mock.Class2, TestSuite.TestEngine.Mock");
        }
    }
}
