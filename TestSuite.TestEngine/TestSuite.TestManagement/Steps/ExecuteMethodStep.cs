using System;

namespace TestSuite.TestManagement
{
    public class ExecuteMethodStep : TestStep
    {
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public ExecutionStatus Status { get; set; }
        public string Error { get; set; }
        public string MethodName { get; set; }
        public string Parameters { get; set; }
    }
}