using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class ExecuteMethodStepHtmlBuilder
    {
        private List<ExecuteMethodStep> steps = new List<ExecuteMethodStep>();

        private HtmlHelper html;
        private StringBuilder htmlBuilder;

        public IEnumerable<ExecuteMethodStep> Steps
        {
            get { return this.steps; }
        }

        private int stepCount = 0;
        public int StepCount
        {
            get { return ++stepCount; }
        }
        
        public ExecuteMethodStepHtmlBuilder(HtmlHelper html, StringBuilder htmlBuilder)
        {
            this.html = html;
            this.htmlBuilder = htmlBuilder;
        }

        public void Build()
        {
            if (steps.Any())
            {
                htmlBuilder.AppendLine(
                    html.Partial("_Step", this).ToString());
                steps.Clear();
            }
        }

        public void Add(ExecuteMethodStep executeMethodStep)
        {
            steps.Add(executeMethodStep);
        }
    }
}