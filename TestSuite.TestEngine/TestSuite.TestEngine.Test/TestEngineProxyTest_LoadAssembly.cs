using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineProxyTest_LoadAssembly
    {
        private ITestEngine testEngine = new TestEngineProxy();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void LoadAssembly_ShouldNotLoadAssemblyInAppDomain()
        {
            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.OutOfDomainMock.dll");

            // Assert
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var mockAssembly = assemblies.FirstOrDefault(a => a.GetName().Name == "TestSuite.TestEngine.OutOfDomainMock");
            mockAssembly.ShouldBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadAssembly_ShouldThrowException_WhenFileNotFound()
        {
            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock1.dll");
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void LoadAssembly_ShouldThrowException_WhenAssemblyExists()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
        }
    }
}
