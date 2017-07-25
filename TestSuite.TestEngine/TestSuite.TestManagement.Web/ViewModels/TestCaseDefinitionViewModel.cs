namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseDefinitionViewModel
    {
        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public TestCaseDefinitionViewModel()
        {
        }

        public TestCaseDefinitionViewModel(string name)
        {
            this.Name = name;
        }
    }
}