using System.Linq;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseSummaryViewModel
    {
        public string Name { get; set; }
        public int ResultCount { get; set; }
        public ExecutionStatus Status { get; set; }

        public TestCaseSummaryViewModel() { }
        
        public TestCaseSummaryViewModel(TestCase testCase)
        {
            this.Name = testCase.Name;
            if(testCase.Executions.Any())
            {
                this.ResultCount = testCase.Executions.Count();
                this.Status = testCase.Executions.First().Status;
            }
        }
    }
}