namespace TestSuite.TestEngine
{
    public interface IMethodExecution
    {
        void Execute(Method method);
        void Execute(string method);
    }
}