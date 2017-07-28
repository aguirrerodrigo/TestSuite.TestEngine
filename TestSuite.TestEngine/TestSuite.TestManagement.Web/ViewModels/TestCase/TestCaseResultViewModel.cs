using System;

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

        public string Template
        {
            get
            {
                var templateClassVisitor = new TemplateClassVisitor();
                this.result.Steps.Accept(templateClassVisitor);
                return templateClassVisitor.Build();
            }
        }

        public TestCaseResultViewModel(TestCaseExecution result)
        {
            this.result = result;
            this.Name = result.Name;
            this.CreatedDateTime = result.CreatedDateTime;
            this.Error = result.Error;
            this.Started = result.Started ?? DateTime.Now.AddSeconds(-1234);
            this.Ended = result.Ended ?? DateTime.Now;
            this.Duration = (this.Ended ?? DateTime.Now) - this.Started;
            this.Status = result.Status;
        }
    }
}