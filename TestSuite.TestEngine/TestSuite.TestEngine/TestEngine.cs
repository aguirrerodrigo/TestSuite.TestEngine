using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestSuite.TestEngine
{
    [Serializable]
    public partial class TestEngine : MarshalByRefObject, ITestEngine
    {
        private Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                return this.assemblies.Select(kvp => kvp.Value);
            }
        }

        public object TestInstance { get; private set; }

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
            var assemblyName = assembly.GetName().Name;
            if (this.assemblies.ContainsKey(assemblyName))
                throw new TestEngineConfigurationException($"Could not add assembly '{assemblyName}'. Assembly with the same name already exists.");
            
            this.assemblies.Add(assemblyName, assembly);
        }
        
        public void SetClass(string qualifiedName)
        {
            try
            {
                this.ExtractAssembly(qualifiedName, out var assemblyName, out var typeName);
                this.TestInstance = this.CreateInstance(assemblyName, typeName);
                this.methodExecution = new MethodExecution(this.TestInstance);
            }
            catch (Exception ex)
            {
                throw new TestEngineConfigurationException($"Could not set class with qualified name '{qualifiedName}'. {ex.Message}");
            }
        }

        private void ExtractAssembly(string qualifiedName, out string assemblyName, out string className)
        {
            assemblyName = null;
            className = null;
            var split = qualifiedName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 2)
                throw new TestEngineConfigurationException($"Invalid qualified name format.");

            className = split[0].Trim();
            assemblyName = split[1].Trim();
        }

        private object CreateInstance(string assemblyName, string typeName)
        {
            var assembly = GetAssembly(assemblyName);
            var instance = assembly.CreateInstance(typeName);
            if (instance == null)
                throw new TestEngineConfigurationException($"Could not find type with name '{typeName}'.");

            return instance;
        }

        private Assembly GetAssembly(string assemblyName)
        {
            if (!this.assemblies.ContainsKey(assemblyName))
                throw new TestEngineConfigurationException($"Could not find assembly with name '{assemblyName}'.Load the assembly first using the LoadAssembly method.");
            return this.assemblies[assemblyName];
        }

        public void Dispose()
        {
        }
    }
}
