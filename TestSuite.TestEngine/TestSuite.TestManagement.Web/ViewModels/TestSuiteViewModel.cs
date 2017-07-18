using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestSuiteViewModel
    {
        [DisplayName("Test Case")]
        [RegularExpression(@"^[^\\/:?""<>|]+$", ErrorMessage = @"Test case cannot contain: \ / : * ? \"" < > |")]
        [Required(ErrorMessage = @"Test case is required.")]
        public string TestCase { get; set; }

        public IEnumerable<TestCase> TestCases { get; set; }

        public TestSuiteViewModel()
        {
        }

        public TestSuiteViewModel(string testCase)
        {
            this.TestCase = testCase;
        }
    }
}