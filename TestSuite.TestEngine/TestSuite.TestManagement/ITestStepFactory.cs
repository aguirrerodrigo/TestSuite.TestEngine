namespace TestSuite.TestManagement
{
    public interface ITestStepFactory
    {
        TestStep Create(string command);
    }
}