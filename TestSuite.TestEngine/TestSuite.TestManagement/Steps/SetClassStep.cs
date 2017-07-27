using System;

namespace TestSuite.TestManagement
{
    public class SetClassStep : TestStep
    {
        public string QualifiedName { get; set; }
        public ExecutionStatus Status { get; set; }
        public string Error { get; set; }

        public override void Accept(ITestStepVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}