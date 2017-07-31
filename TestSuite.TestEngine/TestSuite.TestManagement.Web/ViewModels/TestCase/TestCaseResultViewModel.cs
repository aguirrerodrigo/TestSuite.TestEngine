using System;
using System.Web.Mvc;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestCaseResultViewModel
    {
        private TestCaseExecution result;

        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Error { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public ExecutionStatus Status { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool IsSelected { get; set; }

        public string GenerateTemplate()
        {
            var templateClassVisitor = new TemplateClassVisitor();
            this.result.Steps.Accept(templateClassVisitor);
            return templateClassVisitor.Build();
        }

        public string RenderHtml(HtmlHelper html)
        {
            var htmlVisitor = new HtmlVisitor(html);
            this.result.Steps.Accept(htmlVisitor);
            return htmlVisitor.Build();
        }

        public TestCaseResultViewModel(TestCaseExecution result)
        {
            this.result = result;
            this.Name = result.Name;
            this.CreatedDateTime = result.CreatedDateTime;
            this.Error = result.Error;
            this.Started = result.Started;
            this.Ended = result.Ended;
            this.Duration = (this.Ended ?? DateTime.Now) - this.Started;
            this.Status = result.Status;
        }
    }
}