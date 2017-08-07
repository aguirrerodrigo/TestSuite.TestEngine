using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineProxyTest
    {
        private ITestEngine testEngine = new TestEngineProxy();

        [TestCleanup]
        public void Cleanup()
        {
            testEngine.Dispose();
        }

        [TestMethod]
        public void MethodExecution_ShouldExecuteMethod()
        {
            var filePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            try
            {
                // Arrange
                testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
                testEngine.SetClass(@"TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

                var method = new Method("WriteToFile");
                method.Parameters["file"] = filePath;
                method.Parameters["contents"] = "FileWritten";

                // Act
                testEngine.MethodExecution.Execute(method);

                // Assert
                var readFile = File.ReadAllText(filePath);
                readFile.ShouldEqual("FileWritten");
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void MethodExecution_ShouldNotChangeStaticValue()
        {
            // Arrange
            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
            testEngine.SetClass(@"TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

            // Act
            testEngine.MethodExecution.Execute("MethodAccessingStatic");

            // Assert
            MockClass.methodAccessingStaticExecuted.ShouldBeFalse();
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
    }
}
