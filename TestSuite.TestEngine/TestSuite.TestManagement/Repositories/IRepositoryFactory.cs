using System;

namespace TestSuite.TestManagement.Repositories
{
    public interface IRepositoryFactory
    {
        ITestCaseRepository CreateTestCaseRepository();
    }
}