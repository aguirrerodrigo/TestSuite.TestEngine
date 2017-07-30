using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using TestSuite.TestManagement.Web.ViewModels;

namespace TestSuite.TestManagement.Web.Tests.ViewModels
{
    [TestClass]
    public class TestCaseResultViewModelTest
    {
        private TestCaseResultViewModel model;

        public TestCaseResultViewModelTest()
        {
            this.model = new TestCaseResultViewModel(new TestCaseExecution());
        }

        [TestMethod]
        public void GenerateTemplate()
        {
            // Arrange

            // Act
            var result = model.GenerateTemplate();

            // Assert
            result.ShouldNotBeNull();
        }
    }
}
