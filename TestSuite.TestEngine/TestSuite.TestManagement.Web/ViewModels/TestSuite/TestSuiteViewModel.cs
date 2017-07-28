using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestSuiteViewModel
    {
        [DisplayName("Test Case")]
        [RegularExpression(@"^[^\\/:?""<>|]+$", ErrorMessage = @"Test case cannot contain: \ / : * ? \"" < > |")]
        [Required(ErrorMessage = @"Test case is required.")]
        public string TestCase { get; set; }

        public IEnumerable<TestCaseSummaryViewModel> TestCases { get; set; }

        public TestSuiteViewModel() { }

        public TestSuiteViewModel(string testCase)
        {
            this.TestCase = testCase;
        }

        public void SetSummary(IEnumerable<TestCase> testCases)
        {
            this.TestCases = testCases.Select(tc => new TestCaseSummaryViewModel(tc));
        }
    }
}