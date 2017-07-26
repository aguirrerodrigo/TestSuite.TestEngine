using System;
using System.Collections.Generic;
using System.Linq;
using TestSuite.TestManagement.Extensions;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement
{
    public class TestCase
    {
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        
        private List<TestCaseDefinition> definitions = new List<TestCaseDefinition>();
        public IEnumerable<TestCaseDefinition> Definitions
        {
            get { return this.definitions; }
            set
            {
                var sorted = value.OrderByDescending(d => d.CreatedDateTime);
                this.definitions = new List<TestCaseDefinition>(sorted);
            }
        }

        private List<TestCaseResult> results = new List<TestCaseResult>();
        public IEnumerable<TestCaseResult> Results
        {
            get { return this.results; }
            set
            {
                var sorted = value.OrderByDescending(r => r.ModifiedDateTime);
                this.results = new List<TestCaseResult>(sorted);
            }
        }

        public void Create(ITestCaseRepository repository)
        {
            repository.Create(this);
        }

        public virtual TestCaseDefinition UpdateDefinition(string definition, ITestCaseRepository repository)
        {
            var timeStamp = DateTime.Now.ToTimeStamp();
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = string.Format($"Definition_{timeStamp}");
            testCaseDefinition.Definition = definition;

            repository.AddDefinition(this.Name, testCaseDefinition);
            this.definitions.Insert(0, testCaseDefinition);

            return testCaseDefinition;
        }
    }
}