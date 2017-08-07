//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Should;

//namespace TestSuite.TestEngine.Test
//{
//    [TestClass]
//    public class TestEngine_TestMethodExecution
//    {
//        public ITestEngine testEngine = new TestEngine();

//        [TestCleanup]
//        public void Cleanup()
//        {
//            testEngine.Dispose();
//        }

//        [TestMethod]
//        public void Test()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            var method = new Method("Method1");
//            method.Parameters["param1"] = "string";
//            method.Parameters["param2"] = 42;
//            testEngine.MethodExecution.Execute(method);

//            // Assert
//            MockClass.method1Executed.ShouldBeTrue();
//        }

//        [TestMethod]
//        [ExpectedException(typeof(NotImplementedException))]
//        public void Test_ThrowException_ShouldThrowOriginalException()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            testEngine.MethodExecution.Execute("ThrowException");

//            // Assert
//        }

//        [TestMethod]
//        public void TestNoParams()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            testEngine.MethodExecution.Execute("Method3");

//            // Assert
//            MockClass.method3Executed.ShouldBeTrue();
//        }

//        [TestMethod]
//        [ExpectedException(typeof(TestEngineConfigurationException))]
//        public void TestClassNotDefined_ThrowsException()
//        {
//            // Arrange

//            // Act
//            testEngine.MethodExecution.Execute(new Method("Method1"));

//            // Assert
//        }

//        [TestMethod]
//        [ExpectedException(typeof(MethodExecutionException))]
//        public void TestWrongParamTypes_ThrowsException()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            var method = new Method("Method1");
//            method.Parameters["param1"] = "string";
//            method.Parameters["param2"] = "42";
//            testEngine.MethodExecution.Execute(method);

//            // Assert
//        }

//        [TestMethod]
//        [ExpectedException(typeof(MethodExecutionException))]
//        public void TestWrongParamTypesWithNull_ThrowsException()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            var method = new Method("Method1");
//            method.Parameters["param1"] = "string";
//            method.Parameters["param2"] = null;
//            testEngine.MethodExecution.Execute(method);

//            // Assert
//        }

//        [TestMethod]
//        [ExpectedException(typeof(MethodExecutionException))]
//        public void TestWrongParamNames_ThrowsException()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            var method = new Method("Method1");
//            method.Parameters["param1"] = "string";
//            method.Parameters["param3"] = 42;
//            testEngine.MethodExecution.Execute(method);

//            // Assert
//        }

//        [TestMethod]
//        [ExpectedException(typeof(MethodExecutionException))]
//        public void TestWrongParamCount_ThrowsException()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            var method = new Method("Method1");
//            method.Parameters["param1"] = "string";
//            testEngine.MethodExecution.Execute(method);

//            // Assert
//        }

//        [TestMethod]
//        [ExpectedException(typeof(MethodExecutionException))]
//        public void TestMethodNotFound_ThrowsException()
//        {
//            // Arrange
//            testEngine.LoadAssembly(@"TestSuite.TestEngine.Test.dll");
//            testEngine.SetClass("TestSuite.TestEngine.Test.MockClass, TestSuite.TestEngine.Test");

//            // Act
//            testEngine.MethodExecution.Execute("Method4");

//            // Assert
//        }
//    }
//}
