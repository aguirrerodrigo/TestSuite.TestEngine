﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class TestCaseExecutionTest_FromXml
    {
        [TestMethod]
        public void Test_ShouldStillCreateInstance_OnErrorParsingXml()
        {
            // Arrange
            var xml = "<invalid xml></format>";

            // Act
            var instance = TestCaseExecution.FromXml(xml);

            // Assert
            instance.ShouldNotBeNull();
            instance.Error.ShouldNotBeNull();
        }
    }
}
