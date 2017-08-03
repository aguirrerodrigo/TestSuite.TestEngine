using System;

namespace TestSuite.TestEngine
{
    public interface ITestEngine : IDisposable
    {
        IMethodExecution MethodExecution { get; }

        void LoadAssembly(string assemblyPath);
        void SetClass(string qualifiedName);
    }
}