using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseViewModel
    {
        public string Name { get; set; }

        [DisplayName("Update definition")]
        public string Definition { get; set; }

        [DisplayName("History")]
        public IEnumerable<TestCaseDefinitionViewModel> Definitions { get; set; }

        public TestCaseViewModel()
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

            this.Definitions = definitions;
        }
    }
}