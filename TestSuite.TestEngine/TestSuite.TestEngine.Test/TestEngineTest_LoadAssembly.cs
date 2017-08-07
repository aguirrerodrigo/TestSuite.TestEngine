using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineTest_LoadAssembly
    { 
        private TestEngine testEngine = new TestEngine();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void ShouldAddAssembly()
        {
            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Assert
            var assembly = testEngine.Assemblies.FirstOrDefault(a => a.GetName().Name == "TestSuite.TestEngine.Mock");
            assembly.ShouldNotBeNull();
        }

        [TestMethod]
        public void ShouldLoadAssemblyToAppDomain()
        {
            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Assert
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "TestSuite.TestEngine.Mock");
            assembly.ShouldNotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowException_WhenFileNotFound()
        {
            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock1.dll");
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void ShouldThrowException_WhenAssemblyExists()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
        }
    }
}
