using System;

namespace TestSuite.TestEngine
{
    public class TestEngineProxy : ITestEngine
    {
        private AppDomain domainProxy;
        private ITestEngine instance;

        public TestEngineProxy()
        {
            this.domainProxy = AppDomain.CreateDomain("TestSuite.TestEngine", null, AppDomain.CurrentDomain.SetupInformation);

            var testEngineType = typeof(TestEngine);
            this.instance = (ITestEngine)this.domainProxy.CreateInstanceAndUnwrap(testEngineType.Assembly.GetName().Name, testEngineType.FullName);
        }

        public IMethodExecution MethodExecution
        {
            get { return this.instance.MethodExecution; }
        }

        public void LoadAssembly(string assemblyPath)
        {
            this.instance.LoadAssembly(assemblyPath);
        }

        public void SetClass(string qualifiedName)
        {
            this.instance.SetClass(qualifiedName);
        }

        public void Dispose()
        {
            AppDomain.Unload(domainProxy);
        }
    }
}
