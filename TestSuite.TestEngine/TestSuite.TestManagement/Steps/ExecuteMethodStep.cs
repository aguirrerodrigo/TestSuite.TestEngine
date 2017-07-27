using System;
using System.Collections.Generic;

namespace TestSuite.TestManagement
{
    public class ExecuteMethodStep : TestStep
    {
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public ExecutionStatus Status { get; set; }
        public string Error { get; set; }
        public string MethodName { get; set; }
        public List<MethodParameter> Parameters { get; set; } = new List<MethodParameter>();

        public override void Accept(ITestStepVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetFormattedMethodName()
        {
            return this.MethodName?.Trim().Replace(" ", "_");
        }
    }
}