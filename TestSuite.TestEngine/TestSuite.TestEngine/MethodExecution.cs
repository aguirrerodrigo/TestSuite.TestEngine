using System.Linq;
using System.Reflection;

namespace TestSuite.TestEngine
{
    internal class MethodExecution : IMethodExecution
    {
        private object instance;

        public MethodExecution(object instance)
        {
            this.instance = instance;
        }

        public void Execute(Method method)
        {
            var methodInfo = this.GetMethod(method.Name, method.Parameters, out var indexedParameters);
            methodInfo.Invoke(this.instance, indexedParameters);
        }

        public void Execute(string method)
        {
            this.Execute(new Method(method));
        }

        private MethodInfo GetMethod(string name, ParameterCollection parameters, out object[] indexedParameters)
        {
            var type = this.instance.GetType();
            var methods = type.GetMethods().Where(m => m.Name == name).ToArray();
            if (methods.Length == 0)
                throw new MethodExecutionException($"Could not find method with name '{name}' in type '{type.Name}'.");

            foreach (var method in methods)
            {
                var paramInfos = method.GetParameters();
                indexedParameters = new object[paramInfos.Length];

                if (paramInfos.Length != parameters.Count)
                    break;

                var doesParamsMatch = true;
                for (int i = 0; i < paramInfos.Length; i++)
                {
                    var paramInfo = paramInfos[i];
                    if (!parameters.ContainsKey(paramInfo.Name)
                        || !paramInfo.ParameterType.IsAssignableFrom(
                                parameters[paramInfo.Name]?.GetType() ?? typeof(object)))
                    {
                        doesParamsMatch = false;
                        break;
                    }
                    else
                    {
                        indexedParameters[i] = parameters[paramInfo.Name];
                    }
                }

                if (doesParamsMatch)
                    return method;
            }

            var paramString = string.Join(", ", parameters.Select(p => $"{p.Key} as {p.Value?.GetType()?.Name ?? "Object"}"));
            throw new MethodExecutionException($"Could not find method with name '{name}' and parameters {{{paramString}}} in type '{type.Name}'.");
        }
    }
}
