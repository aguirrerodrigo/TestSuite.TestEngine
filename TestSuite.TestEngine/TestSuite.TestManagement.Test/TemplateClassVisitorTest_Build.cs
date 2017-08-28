using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TemplateClassVisitorTest_Build
    {
        private TemplateClassVisitor visitor = new TemplateClassVisitor();

        [TestMethod]
        public void ShouldIncludeUnitTestingNameSpace()
        {
            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain("using Microsoft.VisualStudio.TestTools.UnitTesting;");
        }

        [TestMethod]
        public void ShouldIncludeTestClass()
        {
            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain("[TestClass]");
        }

        [TestMethod]
        public void ShouldCreatePlaceHolder_WhenNoClassSet()
        {
            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain("namespace <NAMESPACE>");
            result.ShouldContain("public class <CLASS>");
        }

        [TestMethod]
        public void ShouldCreatePlaceHolder_WhenClassHasNoNamespace()
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
        public void ShouldSetNameSpaceAndClass()
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
        public void ShouldIncludeTestMethod()
        {
            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain("[TestMethod]");
            result.ShouldContain("public void Test()");
        }

        [TestMethod]
        public void ShouldCreateMethodWithMultipleParameters()
        {
            // Arrange
            var executeMethodStep = new ExecuteMethodStep();
            executeMethodStep.MethodName = "Method1";
            executeMethodStep.Parameters.Add(new MethodParameter("age", "32"));
            executeMethodStep.Parameters.Add(new MethodParameter("name", "rodrigo"));
            executeMethodStep.Parameters.Add(new MethodParameter("age", "33"));
            executeMethodStep.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldContain(@"this.Method1(""32"", ""rodrigo"", ""33"");");
            result.ShouldContain("public void Method1(string age, string name, string age)");
        }

        [TestMethod]
        public void ShouldCreateUniqueMethodSignatures_WhenSameMethodAndParameters()
        {
            // Arrange
            var executeMethodStep1 = new ExecuteMethodStep();
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
            result.IndexOf("public void Method1(string age)")
                .ShouldEqual(result.LastIndexOf("public void Method1(string age)")); // same index, no duplicates.
        }

        [TestMethod]
        public void ShouldExecuteMethodsInOrder()
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

        [TestMethod]
        public void ShouldFormatMethodName()
        {
            // Arrange
            var executeMethodStep = new ExecuteMethodStep();
            executeMethodStep.MethodName = "Method With Spaces";
            executeMethodStep.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldNotContain("Method With Spaces");
            result.ShouldContain("Method_With_Spaces");
        }

        [TestMethod]
        public void ShouldFormatParameterName()
        {
            // Arrange
            var executeMethodStep = new ExecuteMethodStep();
            executeMethodStep.MethodName = "Method1";
            executeMethodStep.Parameters.Add(new MethodParameter("parameter with spaces", "value"));
            executeMethodStep.Accept(visitor);

            // Act
            var result = visitor.Build();

            // Assert
            result.ShouldNotContain("parameter with spaces");
            result.ShouldContain("parameter_with_spaces");
        }
    }
}
