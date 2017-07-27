using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TemplateClassVisitor_TestBuild
    {
        private TemplateClassVisitor visitor = new TemplateClassVisitor();

        [TestMethod]
        public void Test_Empty()
        {
            // Arrange

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldNotBeNull();
        }

        [TestMethod]
        public void Test_NoNamespace()
        {
            // Arrange
            var setClassStep = new SetClassStep();
            setClassStep.QualifiedName = "TestClass1, TestAssembly";
            setClassStep.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain("namespace <NAMESPACE>");
            result.ShouldNotContain("public class <CLASS>");
        }

        [TestMethod]
        public void Test_SetClassStep()
        {
            // Arrange
            var setClassStep = new SetClassStep();
            setClassStep.QualifiedName = "TestAssembly.TestNameSpace.TestClass1, TestAssembly";
            setClassStep.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain("namespace TestAssembly.TestNameSpace");
            result.ShouldContain("public class TestClass1");
        }

        [TestMethod]
        public void Test_SetMethodExecutionStepMultipleParameters()
        {
            // Arrange
            var executeMethodStep = new ExecuteMethodStep();
            executeMethodStep.MethodName = "Method1";
            executeMethodStep.Parameters.Add(new MethodParameter("param1", "value"));
            executeMethodStep.Parameters.Add(new MethodParameter("age", "32"));
            executeMethodStep.Parameters.Add(new MethodParameter("name", "rodrigo"));
            executeMethodStep.Parameters.Add(new MethodParameter("param1", "new value"));
            executeMethodStep.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain(@"this.Method1(""value"", ""32"", ""rodrigo"", ""new value"");");
            result.ShouldContain("public void Method1(string param1, string age, string name, string param1)");
        }

        [TestMethod]
        public void Test_SetMethodExecutionMultipleSignatures()
        {
            // Arrange
            var executeMethodStep1 = new ExecuteMethodStep();
            executeMethodStep1.MethodName = "Method1";
            executeMethodStep1.Parameters.Add(new MethodParameter("param1", "value"));
            executeMethodStep1.Parameters.Add(new MethodParameter("param2", "32"));

            var executeMethodStep2 = new ExecuteMethodStep();
            executeMethodStep2.MethodName = "Method1";
            executeMethodStep2.Parameters.Add(new MethodParameter("age", "32"));

            var executeMethodStep3 = new ExecuteMethodStep();
            executeMethodStep3.MethodName = "Method1";
            executeMethodStep3.Parameters.Add(new MethodParameter("age", "132"));

            var collection = new TestStepCollection() { executeMethodStep1, executeMethodStep2, executeMethodStep3 };
            collection.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.IndexOf(@"this.Method1(""32"");")
                .ShouldBeLessThan(result.IndexOf(@"this.Method1(""132"");"));
            result.IndexOf("public class Method1(string age)")
                .ShouldEqual(result.LastIndexOf("public class Method1(string age)")); // same index, no duplicates.
        }

        [TestMethod]
        public void Test_SetMultipleMethodsOrder()
        {
            // Arrange
            var executeMethodStep1 = new ExecuteMethodStep();
            executeMethodStep1.MethodName = "MethodA";
            
            var executeMethodStep2 = new ExecuteMethodStep();
            executeMethodStep2.MethodName = "MethodB";

            var executeMethodStep3 = new ExecuteMethodStep();
            executeMethodStep3.MethodName = "MethodC";

            var collection = new TestStepCollection() { executeMethodStep2, executeMethodStep3, executeMethodStep1 };
            collection.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.IndexOf("this.MethodA();")
                .ShouldBeGreaterThan(result.IndexOf("this.MethodB();"));
            result.IndexOf("public void MethodA()")
                .ShouldBeLessThan(result.IndexOf("public void MethodB()"));
        }
    }
}
