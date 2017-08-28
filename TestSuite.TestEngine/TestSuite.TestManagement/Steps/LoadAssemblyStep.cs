namespace TestSuite.TestManagement
{
    public class LoadAssemblyStep : TestStep
    {
        public string AssemblyPath { get; set; }
        public ExecutionStatus Status { get; set; }
        public string Error { get; set; }

        public override void Accept(ITestStepVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
