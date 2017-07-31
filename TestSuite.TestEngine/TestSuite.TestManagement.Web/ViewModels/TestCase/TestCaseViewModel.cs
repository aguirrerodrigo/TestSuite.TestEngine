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

        public TestCaseViewModel(TestCase testCase)
        {
            this.testCase = testCase;
            this.Name = testCase.Name;
            this.Results = testCase.Executions.Select(r => new TestCaseResultViewModel(r)).ToList();
            this.Definitions = testCase.Definitions.Select(d => new TestCaseDefinitionViewModel(d)).ToList();
        }

        public void SelectResult(string resultName)
        {
            var result = default(TestCaseResultViewModel);

            if (string.IsNullOrWhiteSpace(resultName))
            {
                result = this.Results.FirstOrDefault() ??
                    throw new ResourceNotFoundException($"Test case '{this.Name}' does not have any results.");
            }
            else
            {
                result = this.Results.FirstOrDefault(r => r.Name == resultName) ??
                    throw new ResourceNotFoundException($"Could not find result '{resultName}' for test case '{this.Name}'.");
            }
            
            result.IsSelected = true;
            this.SelectedResult = result;
        }

        public void SelectDefinition(string definitionName)
        {
            var definition = default(TestCaseDefinitionViewModel);

            if (string.IsNullOrWhiteSpace(definitionName))
                definition = this.Definitions.FirstOrDefault();
            else
            {
                definition = this.Definitions.FirstOrDefault(d => d.Name == definitionName) ??
                    throw new ResourceNotFoundException($"Could not find definition '{definitionName}' for test case '{this.Name}'.");
            }

            if (definition != null)
            {
                definition.IsSelected = true;
                this.SelectedDefinition = definition;
            }
        }
    }
}