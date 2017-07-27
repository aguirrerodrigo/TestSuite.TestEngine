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

        private List<TestCaseExecution> executions = new List<TestCaseExecution>();
        public IEnumerable<TestCaseExecution> Executions
        {
            get { return this.executions; }
            set
            {
                var sorted = value.OrderByDescending(r => r.CreatedDateTime);
                this.executions = new List<TestCaseExecution>(sorted);
            }
        }

        public void Create(ITestCaseRepository repository)
        {
            repository.Create(this);
        }

        public TestCaseDefinition AddDefinition(string definition, ITestCaseRepository repository)
        {
            var timeStamp = DateTime.Now.ToTimeStamp();
            var testCaseDefinition = new TestCaseDefinition();
            testCaseDefinition.Name = $"Definition_{timeStamp}";
            testCaseDefinition.Definition = definition;

            repository.AddDefinition(this.Name, testCaseDefinition);
            this.definitions.Insert(0, testCaseDefinition);

            return testCaseDefinition;
        }

        public TestCaseExecution AddExecution(string definition, ITestCaseRepository repository)
        {
            return AddExecution(definition, repository, new TestStepFactory());
        }

        public TestCaseExecution AddExecution(string definition, ITestCaseRepository repository, ITestStepFactory factory)
        {
            var timeStamp = DateTime.Now.ToTimeStamp();
            var testCaseExecution = new TestCaseExecution();
            testCaseExecution.Name = $"TestCase_{timeStamp}";
            testCaseExecution.Steps = GetTestSteps(definition, factory);

            repository.AddExecution(this.Name, testCaseExecution);
            this.executions.Insert(0, testCaseExecution);

            return testCaseExecution;
        }

        private TestStepCollection GetTestSteps(string definition, ITestStepFactory factory)
        {
            var steps = new TestStepCollection();
            var split = definition.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in split)
            {
                var testStep = factory.Create(line);
                if (testStep != null)
                    steps.Add(testStep);
            }

            return steps;
        }
    }
}