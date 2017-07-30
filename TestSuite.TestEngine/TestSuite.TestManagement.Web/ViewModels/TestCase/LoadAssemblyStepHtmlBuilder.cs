using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TestSuite.TestManagement.Web.ViewModels
{
    internal class LoadAssemblyStepHtmlBuilder
    {
        private List<LoadAssemblyStep> steps = new List<LoadAssemblyStep>();

        private HtmlHelper html;
        private StringBuilder htmlBuilder;
        
        public LoadAssemblyStepHtmlBuilder(HtmlHelper html, StringBuilder htmlBuilder)
        {
            this.html = html;
            this.htmlBuilder = htmlBuilder;
        }

        public void Build()
        {
            if (steps.Any())
            {
                htmlBuilder.AppendLine(
                    html.Partial("_Assembly", steps).ToString());
                steps.Clear();
            }
        }

        public void Add(LoadAssemblyStep loadAssemblyStep)
        {
            steps.Add(loadAssemblyStep);
        }
    }
}