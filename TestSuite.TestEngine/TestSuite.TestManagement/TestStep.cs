namespace TestSuite.TestManagement
{
    public abstract class TestStep
    {
        public virtual void Visit(ITestStepVisitor visitor) { }
    }
}