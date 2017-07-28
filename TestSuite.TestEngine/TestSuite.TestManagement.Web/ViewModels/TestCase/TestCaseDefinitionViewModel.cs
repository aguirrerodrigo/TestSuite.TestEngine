using System;
using System.ComponentModel;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseDefinitionViewModel
    {
        private TestCaseDefinition definition;

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }
        
        [DisplayName("Update definiton")]
        public string Definition { get; set; }

        public bool IsSelected { get; set; }

        public TestCaseDefinitionViewModel() { }

        public TestCaseDefinitionViewModel(TestCaseDefinition definition)
        {
            this.definition = definition;
            this.Name = definition.Name;
            this.CreatedDateTime = definition.CreatedDateTime;
            this.Definition = definition.Definition;
        }
    }
}