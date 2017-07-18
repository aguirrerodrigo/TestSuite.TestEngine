using System;
using System.Collections.Generic;
using System.Linq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.FileSystemRepository
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private IFileSystemRepository fileSystemRepository;

        public TestCaseRepository(IFileSystemRepository fileSystemRepository)
        {
            this.fileSystemRepository = fileSystemRepository;
        }

        public TestCaseRepository(string path) : this(new FileSystemRepository(path))
        {
        }
        
        public void AddDefinition(string testCase, TestCaseDefinition definition)
        {
            throw new NotImplementedException();
        }

        public void Create(TestCase testCase)
        {
            var directory = this.fileSystemRepository.CreateDirectory(testCase.Name);
            testCase.CreatedDateTime = directory.CreatedDateTime;
        }

        public IEnumerable<TestCase> FetchAll()
        {
            List<TestCase> result = new List<TestCase>();

            var directories = this.fileSystemRepository.FetchAll().OrderByDescending(d => d.CreatedDateTime);
            foreach(var directory in directories)
            {
                var testCase = new TestCase();
                testCase.CreatedDateTime = directory.CreatedDateTime;
                testCase.Name = directory.Name;
                result.Add(testCase);
            }

            return result;
        }
    }
}
