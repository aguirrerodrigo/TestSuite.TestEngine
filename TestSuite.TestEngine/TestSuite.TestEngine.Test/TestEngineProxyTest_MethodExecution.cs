using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineProxyTest_MethodExecution
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
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void MethodExecution_ShouldThrowException_WhenClassNotSet()
        {
            // Act
            var methodExecution = testEngine.MethodExecution;
        }
    }
}
