using System;
using System.Collections.Generic;

namespace TestSuite.TestManagement.Repositories
{
    public interface ITestCaseRepository
    {
        IEnumerable<TestCase> FetchAll();
        void Create(TestCase testCase);
        void AddDefinition(string testCase, TestCaseDefinition definition);
    }
}