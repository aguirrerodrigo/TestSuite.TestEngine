using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestStepFactoryTest_Create
    {
        private ITestStepFactory testFactory = new TestStepFactory();

        [TestMethod]
        public void ShouldParseLoadAssembly_WithNoParam()
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
        public void ShouldParseLoadAssembly()
        {
            // Arrange
            var command = @"!loadAssembly Assemblies DLL\TestSuite.TestEngine.Mock.dll";

            // Act
            var step = testFactory.Create(command) as LoadAssemblyStep;

            // Assert
            step.AssemblyPath.ShouldEqual(@"Assemblies DLL\TestSuite.TestEngine.Mock.dll");
        }

        [TestMethod]
        public void ShouldParseSetClass_WithNoParam()
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
        public void ShouldParseSetClass()
        {
            // Arrange
            var command = "!setClass TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock";

            // Act
            var step = testFactory.Create(command) as SetClassStep;

            // Assert
            step.QualifiedName.ShouldEqual("TestSuite.TestEngine.Mock.Class1, TestSuite.TestEngine.Mock");
        }

        [TestMethod]
        public void ShouldParseTestStep_WithNoParam()
        {
            // Arrange
            var command = "!testStep";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.ShouldNotBeNull();
            step.MethodName.ShouldBeNull();
            step.Parameters.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldParseTestStep_WithNoMethodParameters()
        {
            // Arrange
            var command = "!testStep ExecuteMethod";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldParseTestStep()
        {
            // Arrange
            var command = "!testStep ExecuteMethod @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters[0].Name.ShouldEqual("param1");
            step.Parameters[0].Value.ShouldEqual("2");
            step.Parameters[1].Name.ShouldEqual("param3");
            step.Parameters[1].Value.ShouldEqual("test string");
        }

        [TestMethod]
        public void ShouldParseTestStep_WithMethodWithSpaces()
        {
            // Arrange
            var command = "!testStep Execute This Method @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("Execute This Method");
            step.Parameters[0].Name.ShouldEqual("param1");
            step.Parameters[0].Value.ShouldEqual("2");
            step.Parameters[1].Name.ShouldEqual("param3");
            step.Parameters[1].Value.ShouldEqual("test string");
        }

        [TestMethod]
        public void ShouldParseTestStep_WithDuplicateParameterNames()
        {
            // Arrange
            var command = "!testStep Execute This Method @param1=2 @param1=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("Execute This Method");
            step.Parameters[0].Name.ShouldEqual("param1");
            step.Parameters[0].Value.ShouldEqual("2");
            step.Parameters[1].Name.ShouldEqual("param1");
            step.Parameters[1].Value.ShouldEqual("test string");
        }

        [TestMethod]
        public void ShouldParseStepFromShortcut_WithNoParam()
        {
            // Arrange
            var command = "#";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.ShouldNotBeNull();
            step.MethodName.ShouldBeNull();
            step.Parameters.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldParseTestStepWithShortcut_WithNoMethodParameters()
        {
            // Arrange
            var command = "# ExecuteMethod";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldParseTestStepWithShortcut()
        {
            // Arrange
            var command = "#ExecuteMethod @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("ExecuteMethod");
            step.Parameters[0].Name.ShouldEqual("param1");
            step.Parameters[0].Value.ShouldEqual("2");
            step.Parameters[1].Name.ShouldEqual("param3");
            step.Parameters[1].Value.ShouldEqual("test string");
        }

        [TestMethod]
        public void ShouldParseTestStepWithShortcut_WithMethodWithSpaces()
        {
            // Arrange
            var command = "#Execute This Method @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("Execute This Method");
            step.Parameters[0].Name.ShouldEqual("param1");
            step.Parameters[0].Value.ShouldEqual("2");
            step.Parameters[1].Name.ShouldEqual("param3");
            step.Parameters[1].Value.ShouldEqual("test string");
        }

        [TestMethod]
        public void ShouldParseTestStepWithShortcut_WithDuplicateParameterNames()
        {
            // Arrange
            var command = "#  Execute This Method @param1=2 @param1=test string";

            // Act
            var step = testFactory.Create(command) as ExecuteMethodStep;

            // Assert
            step.MethodName.ShouldEqual("Execute This Method");
            step.Parameters[0].Name.ShouldEqual("param1");
            step.Parameters[0].Value.ShouldEqual("2");
            step.Parameters[1].Name.ShouldEqual("param1");
            step.Parameters[1].Value.ShouldEqual("test string");
        }

        [TestMethod]
        public void ShouldParseFormattingStep()
        {
            // Arrange
            var command = "~ExecuteMethod @param1=2 @param3=test string";

            // Act
            var step = testFactory.Create(command) as FormattingStep;

            // Assert
            step.FormattingText.ShouldEqual("~ExecuteMethod @param1=2 @param3=test string");
        }

        [TestMethod]
        public void ShouldtParseFormattingStep_AsHtml()
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
