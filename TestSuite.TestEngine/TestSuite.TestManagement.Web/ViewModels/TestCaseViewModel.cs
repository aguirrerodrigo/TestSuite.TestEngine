using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseViewModel
    {
        public string Name { get; set; }

        public int ExecutionCount { get; set; }

        public ExecutionStatus Status { get; set; }

        [DisplayName("Update definition")]
        public string Definition { get; set; }

        [DisplayName("History")]
        public IEnumerable<TestCaseDefinitionViewModel> Definitions { get; set; }

        public TestCaseViewModel()
        {
        }

        public TestCaseViewModel(TestCase testCase) : this(testCase, null)
        {
        }

        public TestCaseViewModel(TestCase testCase, string selectedDefinition)
        {
            this.Name = testCase.Name;
            var definitions = new List<TestCaseDefinitionViewModel>();

            foreach (var definition in testCase.Definitions)
            {
                var def = new TestCaseDefinitionViewModel(definition.Name);
                if(def.Name == selectedDefinition && this.Definition == null)
                {
                    def.IsSelected = true;
                    this.Definition = definition.Definition;
                }

                definitions.Add(def);
            }

            if(testCase.Executions.Any())
            {
                this.ExecutionCount = testCase.Executions.Count();
                this.Status = testCase.Executions.First().Status;
            }

            this.Definitions = definitions;
        }
    }
}