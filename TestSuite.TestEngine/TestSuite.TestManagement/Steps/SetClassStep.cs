namespace TestSuite.TestManagement
{
    public class SetClassStep : TestStep
    {
        public string QualifiedName { get; set; }
        public ExecutionStatus Status { get; set; }
        public string Error { get; set; }
    }
}