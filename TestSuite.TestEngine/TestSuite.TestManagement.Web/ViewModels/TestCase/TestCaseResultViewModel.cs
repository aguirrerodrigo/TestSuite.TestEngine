using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TestSuite.TestManagement.Web.Extensions;

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
            //var builder = new StringBuilder();
            //builder.AppendLine(@"<div class=""form-group"">");
            //builder.AppendLine(@"   <label class=""col-sm-2 control-label"">Assembly</label>");
            //builder.AppendLine(@"   <div class=""col-sm-10 text-" + ExecutionStatus.Failed.ToCss() + @" alert alert-danger"">");
            //builder.AppendLine(@"       <span class=""fa fa-" + ExecutionStatus.Failed.ToCssIcon() + @"-circle""></span>");
            //builder.AppendLine(@"       <span>Assemblies\TestSuite.TestEngine.Mock.dll</span>");
            //builder.AppendLine(@"       <p>Unable to load assembly Assemblies\TestSuite.TestEngine.Mock.dll</p>");
            //builder.AppendLine(@"   </div>");
            //builder.AppendLine(@"</div>");

            //return builder.ToString();
            //html.RenderPartial("_Assembly", new LoadAssemblyStep());

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
            this.Started = result.Started ?? DateTime.Now.AddSeconds(-1234);
            this.Ended = result.Ended ?? DateTime.Now;
            this.Duration = (this.Ended ?? DateTime.Now) - this.Started;
            this.Status = result.Status;
        }

        public TestCaseResultViewModel()
        {
        }
    }
}