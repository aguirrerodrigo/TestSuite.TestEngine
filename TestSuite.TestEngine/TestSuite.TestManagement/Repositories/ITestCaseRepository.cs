using System;

namespace TestSuite.TestManagement.Repositories
{
    public interface ITestCaseRepository : IDisposable
    {
        void Save(TestCase testCase);
        void AddDefinition(string testCase, TestCaseDefinition definition);
    }
}