using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseViewModel
    {
        private TestCase testCase;

        public string Name { get; set; }

        public TestCaseResultViewModel SelectedResult { get; set; }

        [DisplayName("History")]
        public IEnumerable<TestCaseResultViewModel> Results { get; set; }

        public TestCaseDefinitionViewModel SelectedDefinition { get; set; }

        [DisplayName("History")]
        public IEnumerable<TestCaseDefinitionViewModel> Definitions { get; set; }

        public TestCaseViewModel() { }

        public TestCaseViewModel(TestCase testCase)
        {
            this.testCase = testCase;
            this.Name = testCase.Name;
            this.Results = testCase.Executions.Select(r => new TestCaseResultViewModel(r)).ToList();
            this.Definitions = testCase.Definitions.Select(d => new TestCaseDefinitionViewModel(d)).ToList();
        }

        public void SelectResult(string resultName)
        {
            var result = this.Results.FirstOrDefault(r => r.Name == resultName);
            if (result != null)
            {
                result.IsSelected = true;
                this.SelectedResult = result;
            }
        }

        public void SelectDefinition(string definitionName)
        {
            var definition = this.Definitions.FirstOrDefault(d => d.Name == definitionName);
            if(definition != null)
            {
                definition.IsSelected = true;
                this.SelectedDefinition = definition;
            }
        }
    }
}