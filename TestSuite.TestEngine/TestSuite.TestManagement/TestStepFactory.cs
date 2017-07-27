using System;
using System.Collections.Generic;

namespace TestSuite.TestManagement
{
    public class TestStepFactory : ITestStepFactory
    {
        public TestStep Create(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return null;

            command = command.Trim();
            if (command.StartsWith("!loadAssembly"))
            {
                var step = new LoadAssemblyStep();
                step.AssemblyPath = ParseCommandParameter(command);
                return step;
            }

            else if (command.StartsWith("!setClass"))
            {
                var step = new SetClassStep();
                step.QualifiedName = ParseCommandParameter(command);
                return step;
            }

            else if (command.StartsWith("!testStep"))
            {
                var commandParameter = ParseCommandParameter(command);
                var step = CreateExecuteMethodStep(commandParameter);
                return step;
            }

            else if (command.StartsWith("#"))
            {
                var commandParameter = command.Substring(1);
                var step = CreateExecuteMethodStep(commandParameter);
                return step;
            }

            else
            {
                var step = new FormattingStep();
                step.FormattingText = command;
                return step;
            }
        }

        private ExecuteMethodStep CreateExecuteMethodStep(string commandParameter)
        {
            var step = new ExecuteMethodStep();
            if (commandParameter != null)
            {
                var split = commandParameter.Split(new string[] { "@" }, 2, StringSplitOptions.RemoveEmptyEntries);
                if(split.Length > 0)
                    step.MethodName = split[0].Trim();
                if (split.Length > 1)
                {
                    step.Parameters = ParseMethodParameters(split[1]);
                }
            }
            return step;
        }

        private List<MethodParameter> ParseMethodParameters(string methodParameters)
        {
            var result = new List<MethodParameter>();
            var paramsSplit = methodParameters.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var param in paramsSplit)
            {
                var name = default(string);
                var value = default(string);

                var split = param.Split(new string[] { "=" }, 2, StringSplitOptions.RemoveEmptyEntries);
                if(split.Length > 0)
                    name = split[0].Trim();
                if (split.Length > 1)
                    value = split[1].Trim();

                if (name != null)
                {
                    var methodParam = new MethodParameter();
                    methodParam.Name = name;
                    methodParam.Value = value;

                    result.Add(methodParam);
                }
            }

            return result;
        }

        private string ParseCommandParameter(string command)
        {
            var split = command.Split(new string[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
                return split[1].Trim();
            else
                return null;
        }
    }
}