using System.Collections.Generic;
using System.Reflection;

namespace TestSuite.TestEngine
{
    public class AssemblyCollection
    {
        Dictionary<string, Assembly> dictionary = new Dictionary<string, Assembly>();

        public Assembly this[string assemblyName]
        {
            get
            {
                if (!this.dictionary.ContainsKey(assemblyName))
                    throw new TestEngineConfigurationException($"Could not find assembly with name '{assemblyName}'. Load the assembly first using the LoadAssembly method.");
                return this.dictionary[assemblyName];
            }
        }

        public void Add(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;
            if (this.dictionary.ContainsKey(assemblyName))
                throw new TestEngineConfigurationException($"Could not add assembly '{assemblyName}'. Assembly with the same name already exists.");
            this.dictionary.Add(assemblyName, assembly);
        }
    }
}
