using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System.IO;
using System;
using System.Linq;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngine_TestLoadAssembly
    {
        private TestEngine testEngine = new TestEngine();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void Test()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.MockReference.dll");

            // Assert
            var assembly = testEngine.Assemblies["TestSuite.TestEngine.MockReference"];
            assembly.ShouldNotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void TestDuplicateLoad_ThrowsException()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestFileNotFound_ThrowsException()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll1");

            // Assert
        }

        [TestMethod]
        public void LoadsAssemblyToAppDomain()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Assert
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var mockAssembly = assemblies.FirstOrDefault(a => a.GetName().Name == "TestSuite.TestEngine.Mock");
            mockAssembly.ShouldNotBeNull();
        }
    }
}
