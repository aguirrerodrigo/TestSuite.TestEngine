using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class HtmlVisitor : ITestStepVisitor
    {
        private StringBuilder htmlBuilder = new StringBuilder();

        private HtmlHelper html;
        private LoadAssemblyStepHtmlBuilder assemblyBuilder;
        private ExecuteMethodStepHtmlBuilder stepBuilder;

        public HtmlVisitor(HtmlHelper html)
        {
            this.html = html;
            this.assemblyBuilder = new LoadAssemblyStepHtmlBuilder(html, htmlBuilder);
            this.stepBuilder = new ExecuteMethodStepHtmlBuilder(html, htmlBuilder);
        }

        public void Visit(ExecuteMethodStep executeMethodStep)
        {
            this.assemblyBuilder.Build();

            this.stepBuilder.Add(executeMethodStep);
        }

        public void Visit(FormattingStep formattingStep)
        {
            this.assemblyBuilder.Build();
            this.stepBuilder.Build();

            htmlBuilder.AppendLine(
                HttpUtility.HtmlDecode(formattingStep.FormattingText));
        }

        public void Visit(LoadAssemblyStep loadAssemblyStep)
        {
            this.stepBuilder.Build();

            this.assemblyBuilder.Add(loadAssemblyStep);
        }

        public void Visit(SetClassStep setClassStep)
        {
            this.assemblyBuilder.Build();

            htmlBuilder.AppendLine(
                html.Partial("_Class", setClassStep).ToString());
        }

        public string Build()
        {
            this.assemblyBuilder.Build();
            this.stepBuilder.Build();

            return htmlBuilder.ToString();
        }
    }
}