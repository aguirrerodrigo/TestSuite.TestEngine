using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class MethodExecutionTest_ExecuteWithParameters
    {
        private MockClass mockInstance;
        private MethodExecution methodExecution;

        public MethodExecutionTest_ExecuteWithParameters()
        {
            this.mockInstance = new MockClass();
            this.methodExecution = new MethodExecution(mockInstance);
        }

        [TestMethod]
        public void ShouldExecuteMethod()
        {
            // Arrange
            var method = new Method("MethodWithNoParameters");

            // Act
            methodExecution.Execute(method);

            // Assert
            mockInstance.methodWithNoParametersExecuted.ShouldBeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void ShouldThrowException_WhenMethodNotFound()
        {
            // Arrange
            var method = new Method("MethodNotFound");

            // Act
            methodExecution.Execute("MethodNotFound");
        }
        
        [TestMethod]
        public void ShouldExecuteMethod_WhenMethodHasParameters()
        {
            // Arrange
            var method = new Method("MethodWithParameters");
            method.Parameters["param1"] = "param1";
            method.Parameters["param2"] = "param2";

            // Act
            methodExecution.Execute(method);

            // Assert
            mockInstance.methodWithParametersExecuted.ShouldBeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void ShouldThrowException_WhenParameterCountNotTheSame()
        {
            // Arrange
            var method = new Method("MethodWithParameters");
            method.Parameters["param1"] = "param1";

            // Act
            methodExecution.Execute(method);
        }

        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void ShouldThrowException_WhenParameterTypesNotTheSame()
        {
            // Arrange
            var method = new Method("MethodWithParameters");
            method.Parameters["param1"] = "param1";
            method.Parameters["param2"] = 23;

            // Act
            methodExecution.Execute(method);
        }

        [TestMethod]
        public void ShouldThrowOriginalExceptionMessage_WhenMethodThrowsException()
        {
            // Arrange
            var exceptionMessage = default(string);
            mockInstance.exceptionMessage = "exception message";
            var method = new Method("MethodThrowsException");

            // Act
            try
            {
                methodExecution.Execute(method);
            }
            catch (MethodExecutionException ex)
            {
                exceptionMessage = ex.Message;
            }

            // Assert
            exceptionMessage.ShouldEqual("exception message");
        }
    }
}
