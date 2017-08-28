using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.Test
{
    [TestClass]
    public class MethodParameterTest
    {
        private MethodParameter methodParameter = new MethodParameter();

        [TestMethod]
        public void GetFormattedName_ShouldReplaceSpacesWithUnderscore()
        {
            // Arrange
            methodParameter.Name = "Parameter Name";

            // Act
            var formattedMethodName = methodParameter.GetFormattedName();

            // Assert
            formattedMethodName.ShouldEqual("Parameter_Name");
        }

        [TestMethod]
        public void GetFormattedName_ShouldReturnNull_WhenNameIsNull()
        {
            // Arrange
            methodParameter.Name = null;

            // Act
            var formattedMethodName = methodParameter.GetFormattedName();

            // Assert
            formattedMethodName.ShouldBeNull();
        }
    }
}
