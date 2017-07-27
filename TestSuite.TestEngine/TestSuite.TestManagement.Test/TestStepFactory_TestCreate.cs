using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestStepFactory_TestCreate
    {
        public ITestStepFactory testFactory = new TestStepFactory();

        [TestMethod]
        public void TestParseLoadAssembly_NoParam()
        {
            // Arrange
            var command = "!loadAssembly";

            // Act
            var step = testFactory.Create(command) as LoadAssemblyStep;

            // Assert
            step.ShouldNotBeNull();
            step.AssemblyPath.ShouldBeNull();
        }

        [TestMethod]
        public void TestParseLoadAssembly()
        {
            // Arrange
            var command = @"!loadAssembly Assemblies DLL\TestSuite.TestEngine.Mock.dll";

            // Act
            var step = testFactory.Create(command) as LoadAssemblyStep;

            // Assert
            step.AssemblyPath.ShouldEqual(@"Assemblies DLL\TestSuite.TestEngine.Mock.dll");
        }

        [TestMethod]
        public void TestParseSetClass_NoParam()
        {
            // Arrange
            var command = "!setClass";

            // Act
            var step = testFactory.Create(command) as SetClassStep;

            // Assert
            step.ShouldNotBeNull();
            step.QualifiedName.ShouldBeNull();
        }

        [TestMethod]
        public void TestParseSetClass()
        {
            // Arrange
            var command = "!setClass TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock";

            // Act
            var step = testFactory.Create(command) as SetClassStep;

            // Assert
            step.QualifiedName.ShouldEqual("TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");
        }

        [TestMethod]
        public void TestParseTestStep_NoParam()
        {
            // Arrange
            var command = "!testStep";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.ShouldNotBeNull();
            step.MethodName.ShouldBeNull();
            step.Parameters.ShouldBeNull();
        }

        [TestMethod]
        public void TestParseTestStep_NoMethodParameters()
        {
            // Arrange
            var command = "!testStep ExecuteMethod";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters.ShouldBeNull();
        }

        [TestMethod]
        public void TestParseTestStep()
        {
            // Arrange
            var command = "!testStep ExecuteMethod @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters.ShouldEqual("@param1=2 @param3=test string");
        }

        [TestMethod]
        public void TestParseTestStep_MethodWithSpaces()
        {
            // Arrange
            var command = "!testStep Execute This Method @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("Execute This Method");
            step.Parameters.ShouldEqual("@param1=2 @param3=test string");
        }

        [TestMethod]
        public void TestParseStepFromShortcut_NoParam()
        {
            // Arrange
            var command = "#";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.ShouldNotBeNull();
            step.MethodName.ShouldBeNull();
            step.Parameters.ShouldBeNull();
        }

        [TestMethod]
        public void TestParseTestStepWithShortcut_NoMethodParameters()
        {
            // Arrange
            var command = "# ExecuteMethod";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters.ShouldBeNull();
        }

        [TestMethod]
        public void TestParseTestStepWithShortcut()
        {
            // Arrange
            var command = "#ExecuteMethod @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters.ShouldEqual("@param1=2 @param3=test string");
        }

        [TestMethod]
        public void TestParseTestStepWithShortcut_MethodWithSpaces()
        {
            // Arrange
            var command = "#Execute This Method @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("Execute This Method");
            step.Parameters.ShouldEqual("@param1=2 @param3=test string");
        }

        [TestMethod]
        public void TestParseFormattingStep()
        {
            // Arrange
            var command = "~ExecuteMethod @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as FormattingStep;

            // Assert
            step.FormattingText.ShouldEqual("~ExecuteMethod @param1=2 @param3=test string");
        }

        [TestMethod]
        public void TestParseFormattingStep_Html()
        {
            // Arrange
            var command = "<h1>Formatting Text</h1>";

            // Act
            var step = testFactory.Create(command) as FormattingStep;

            // Assert
            step.FormattingText.ShouldEqual("<h1>Formatting Text</h1>");
        }
    }
}
