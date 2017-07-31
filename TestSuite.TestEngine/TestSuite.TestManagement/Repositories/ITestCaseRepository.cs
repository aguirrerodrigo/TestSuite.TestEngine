using System;
using System.Collections.Generic;

namespace TestSuite.TestManagement.Repositories
{
    public interface ITestCaseRepository
    {
        IEnumerable<TestCase> FetchAll();
        void Create(TestCase testCase);
        TestCase Get(string testCaseName);
        void AddDefinition(string testCaseName, TestCaseDefinition definition);
        void AddExecution(string testCaseName, TestCaseExecution execution);
        void UpdateExecution(string testCaseName, TestCaseExecution execution);
    }
}