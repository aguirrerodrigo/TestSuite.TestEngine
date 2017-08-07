using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestEngine.Test
{
    [TestClass]
    public class MethodExecutionTest_Execute
    {
        private MockClass mockInstance;
        private MethodExecution methodExecution;

        public MethodExecutionTest_Execute()
        {
            this.mockInstance = new MockClass();
            this.methodExecution = new MethodExecution(mockInstance);
        }

        [TestMethod]
        public void ShouldExecuteMethod()
        {
            // Act
            methodExecution.Execute("MethodWithNoParameters");

            // Assert
            mockInstance.methodWithNoParametersExecuted.ShouldBeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void ShouldThrowException_WhenMethodNotFound()
        {
            // Act
            methodExecution.Execute("MethodNotFound");
        }
        
        [TestMethod]
        [ExpectedException(typeof(MethodExecutionException))]
        public void ShouldThrowException_WhenMethodHasParameters()
        {
            // Act
            methodExecution.Execute("MethodWithParameters");
        }

        [TestMethod]
        public void ShouldThrowOriginalExceptionMessage_WhenMethodThrowsException()
        {
            // Arrange
            var exceptionMessage = default(string);
            mockInstance.exceptionMessage = "exception message";

            // Act
            try
            {
                methodExecution.Execute("MethodThrowsException");
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
