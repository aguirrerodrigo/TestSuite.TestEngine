using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSuite.TestManagement
{
    public class TemplateClassVisitor : ITestStepVisitor
    {
        public string className = null;
        public string nameSpace = null;
        public List<ExecuteMethodStep> methods = new List<ExecuteMethodStep>();

        public void Visit(ExecuteMethodStep testStep)
        {
            this.methods.Add(testStep);
        }

        public void Visit(FormattingStep testStep)
        {
        }

        public void Visit(LoadAssemblyStep testStep)
        {
        }

        public void Visit(SetClassStep testStep)
        {
            var split = testStep.QualifiedName.Split(new string[] { "," }, 2, StringSplitOptions.RemoveEmptyEntries);
            var fullClassName = split[0].Trim();
            var lastDotIndex = fullClassName.LastIndexOf(".");
            if (lastDotIndex > 0)
            {
                this.nameSpace = fullClassName.Substring(0, lastDotIndex);
                this.className = fullClassName.Substring(lastDotIndex + 1);
            }
            else
                this.className = fullClassName;
        }

        public string Build()
        {
            var builder = new StringBuilder();
            builder.AppendLine("using Microsoft.VisualStudio.TestTools.UnitTesting;");
            builder.AppendLine();
            builder.AppendLine($"namespace {this.BuildNameSpace()}");
            builder.AppendLine("{");
                builder.AppendLine("\t[TestClass]");
                builder.AppendLine($"\tpublic class {this.BuildClassName()}");
                builder.AppendLine("\t{");
                builder.AppendLine();
                    builder.AppendLine("\t\t[TestMethod]");
                    builder.AppendLine("\t\tpublic void Test()");
                    builder.AppendLine("\t\t{");
                        foreach(var method in this.methods)
                        {
                            builder.AppendLine($"\t\t\tthis.{method.GetFormattedMethodName()}({this.BuildMethodParameterValues(method)});");
                        }
                    builder.AppendLine("\t\t}");
                    builder.AppendLine();

                    var methodSignatures = this.BuildMethodSignatures(methods);
                    foreach (var method in methodSignatures)
                    {
                        builder.AppendLine($"\t\tpublic void {method}");
                        builder.AppendLine("\t\t{");
                        builder.AppendLine("\t\t}");
                        builder.AppendLine();
                    }
                builder.AppendLine("\t}");
            builder.AppendLine("}");

            this.className = null;
            this.nameSpace = null;
            this.methods.Clear();

            return builder.ToString();
        }

        

        private string BuildNameSpace()
        {
            if (string.IsNullOrWhiteSpace(this.nameSpace))
                return "<NAMESPACE>";
            else
                return this.nameSpace;
        }

        private string BuildClassName()
        {
            if (string.IsNullOrWhiteSpace(this.className))
                return "<CLASS>";
            else
                return this.className;
        }

        private string BuildMethodParameterValues(ExecuteMethodStep method)
        {
            var result = string.Join(", ", method.Parameters.Select(p => $"\"{p.Value}\""));
            return result;
        }

        private IEnumerable<string> BuildMethodSignatures(List<ExecuteMethodStep> methods)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (var method in methods.OrderBy(m => m.MethodName))
            {
                result.Add($"{method.GetFormattedMethodName()}({this.BuildMethodParameters(method)})");
            }

            return result;
        }

        private string BuildMethodParameters(ExecuteMethodStep method)
        {
            var result = string.Join(", ", method.Parameters.Select(p => $"string {p.Name}"));
            return result;
        }
    }
}
