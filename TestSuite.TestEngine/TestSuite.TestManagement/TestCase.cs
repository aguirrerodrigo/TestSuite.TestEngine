using System;
using System.Collections.Generic;
using System.Linq;
using TestSuite.TestManagement.Extensions;

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

        public void Save()
        {
            using (var repo = Domain.Factories.Repository.CreateTestCaseRepository())
            {
                repo.Save(this);
            }
        }

        public TestCaseDefinition UpdateDefinition(string definition)
        {
            var timeStamp = DateTime.Now.ToTimeStamp();
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = string.Format($"definition_{timeStamp}");
            testCaseDefinition.Definition = definition;

            using (var repo = Domain.Factories.Repository.CreateTestCaseRepository())
            {
                repo.AddDefinition(this.Name, testCaseDefinition);
            }

            this.definitions.Insert(0, testCaseDefinition);

            return testCaseDefinition;
        }
    }
}