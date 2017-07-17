using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseViewModel
    {
        [DisplayName("Test Case")]
        [RegularExpression(@"^[^\\/:?""<>|]+$", ErrorMessage = @"{0} cannot contain: \ / : * ? \"" < > |")]
        [StringLength(200)]
        [Required]
        public string Name { get; set; }
        public IEnumerable<TestCase> TestCases { get; set; }

        public TestCaseViewModel(string name)
        {
            this.Name = name;
        }
    }
}