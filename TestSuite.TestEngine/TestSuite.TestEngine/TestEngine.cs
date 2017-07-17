using System;
using System.Reflection;

namespace TestSuite.TestEngine
{
    public class TestEngine
    {
        public AssemblyCollection Assemblies { get; private set; }
            = new AssemblyCollection();

        private IMethodExecution methodExecution;
        public IMethodExecution MethodExecution
        {
            get
            {
                if (this.methodExecution == null)
                    throw new TestEngineConfigurationException("TestClass not yet defined. Define the TestClass first using the SetClass method.");
                return this.methodExecution;
            }
        }

        public void LoadAssembly(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            this.Assemblies.Add(assembly);
        }

        public void SetClass(string qualifiedName)
        {
            try
            {
                this.ExtractAssembly(qualifiedName, out var assemblyName, out var typeName);
                var instance = this.CreateInstance(assemblyName, typeName);
                this.methodExecution = new MethodExecution(instance);
            }
            catch (Exception ex)
            {
                throw new TestEngineConfigurationException($"Could not set class with qualified name '{qualifiedName}'. {ex.Message}", ex);
            }
        }

        private void ExtractAssembly(string qualifiedName, out string assemblyName, out string className)
        {
            assemblyName = null;
            className = null;
            var split = qualifiedName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 2)
                throw new TestEngineConfigurationException($"Qualified name format is incorrect.");

            className = split[0].Trim();
            assemblyName = split[1].Trim();
        }

        private object CreateInstance(string assemblyName, string typeName)
        {
            var assembly = this.Assemblies[assemblyName];
            var instance = assembly.CreateInstance(typeName);
            if (instance == null)
                throw new TestEngineConfigurationException($"Could not find type with name '{typeName}'");

            return instance;
        }
    }
}
