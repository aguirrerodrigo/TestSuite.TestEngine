using System;
using System.Collections.Generic;

namespace TestSuite.TestManagement
{
    public class TestCaseExecution
    {
        public string Name { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }

        private List<TestStep> steps = new List<TestStep>();
        public IEnumerable<TestStep> Steps
        {
            get { return this.steps; }
            set { this.steps = new List<TestStep>(value); }
        }

        public TestCaseExecution() { }
        
        public TestCaseExecution(string definition)
        {
            var split = definition.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach(var row in split)
            {
                //Domain.Factories.TestStep.Create(row);
            }
        }
    }
}
