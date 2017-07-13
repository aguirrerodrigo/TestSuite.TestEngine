using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System.IO;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class TestEngineTest
    {
        public TestEngine testEngine = new TestEngine();

        [TestMethod]
        public void TestLoadAssembly()
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
        public void TestLoadAssembly_Duplicate_Failed()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestLoadAssembly_Failed()
        {
            // Arrange

            // Act
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll1");

            // Assert
        }

        [TestMethod]
        public void TestSetClass()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");

            // Assert
            testEngine.MethodExecution.ShouldNotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void TestSetClass_AssemblyNotLoaded()
        {
            // Arrange

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.Class, TestSuite.TestEngine.Mock");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void TestSetClass_ClassNotFound()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");

            // Act
            testEngine.SetClass("TestSuite.TestEngine.Mock.Class, TestSuite.TestEngine.Mock");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(TestEngineConfigurationException))]
        public void TestMethodExecution_ClassNotDefined()
        {
            // Act
            testEngine.MethodExecution.Execute(new Method("Method1"));
        }

        [TestMethod]
        public void TestMethodExecution()
        {
            // Arrange
            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

            // Act
            var method = new Method("Method1");
            method.Parameters["param1"] = "string";
            method.Parameters["param2"] = 42;
            testEngine.MethodExecution.Execute(method);

            // Assert
            MockClass.method1Executed.ShouldBeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void TestMethodExecution_WrongParamTypes()
        {
            // Arrange
            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

            // Act
            var method = new Method("Method1");
            method.Parameters["param1"] = "string";
            method.Parameters["param2"] = "42";
            testEngine.MethodExecution.Execute(method);

            // Assert
        }

        [TestMethod]
        public void TestMethodExecution_NoParams()
        {
            // Arrange
            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

            // Act
            testEngine.MethodExecution.Execute("Method3");

            // Assert
            MockClass.method3Executed.ShouldBeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void TestMethodExecution_MethodNotFound()
        {
            // Arrange
            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

            // Act
            testEngine.MethodExecution.Execute("Method4");

            // Assert
        }

        [TestMethod]
        public void TestSetClass_TestClassWithReference()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
            testEngine.SetClass("TestSuite.TestEngine.Mock.ClassWithReference, TestSuite.TestEngine.Mock");

            // Act

            // Assert
        }

        [TestMethod]
        public void TestSetClass_TestClassWithReferenceLoaded()
        {
            // Arrange
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.Mock.dll");
            testEngine.LoadAssembly(@"Assemblies\TestSuite.TestEngine.MockReference.dll");
            testEngine.SetClass("TestSuite.TestEngine.Mock.ClassWithReference, TestSuite.TestEngine.Mock");

            // Act
            testEngine.MethodExecution.Execute("Method1");

            // Assert
        }
    }
}
