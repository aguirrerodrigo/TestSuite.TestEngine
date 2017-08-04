using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineProxy_TestLoadAssembly
    {
        private ITestEngine testEngine = new TestEngineProxy();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void ShouldNotLoadInCurrentAppDomain()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.OutOfDomainMock.dll");

            // Assert
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var mockAssembly = assemblies.FirstOrDefault(a => a.GetName().Name == "TestSuite.TestEngine.OutOfDomainMock");
            mockAssembly.ShouldBeNull();
        }
    }
}
