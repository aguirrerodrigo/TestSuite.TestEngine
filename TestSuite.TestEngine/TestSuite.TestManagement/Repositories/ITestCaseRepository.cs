using System;

namespace TestSuite.TestManagement.Repositories
{
    public interface ITestCaseRepository
    {
        void Create(TestCase testCase);
        void AddDefinition(string testCase, TestCaseDefinition definition);
    }
}