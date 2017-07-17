using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System.IO;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngine_TestLoadAssembly
    {
        public TestEngine testEngine = new TestEngine();

        [TestMethod]
        public void Test()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Assert
            var assembly = testEngine.Assemblies["TestSuite.TestEngine.Mock"];
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
    }
}
