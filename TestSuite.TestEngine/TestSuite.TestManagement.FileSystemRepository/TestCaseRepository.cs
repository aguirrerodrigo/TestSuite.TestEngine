using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.FileSystemRepository
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private string path;
        private IFileSystemRepository fileSystemRepository;

        public TestCaseRepository(string path, IFileSystemRepository fileSystemRepository)
        {
            this.path = path;
            this.fileSystemRepository = fileSystemRepository;
        }

        public TestCaseRepository(string path) : this(path, new FileSystemRepository())
        {
        }

        public IEnumerable<TestCase> FetchAll()
        {
            List<TestCase> result = new List<TestCase>();

            var directories = this.fileSystemRepository.FetchAllDirectories(this.path).OrderBy(d => d.Name);
            foreach (var directory in directories)
            {
                var testCase = MapTestCase(directory);
                testCase.Definitions = GetTestCaseDefinitions(directory.Name);
                testCase.Executions = GetTestCaseExecutions(directory.Name);
                result.Add(testCase);
            }

            return result;
        }

        public void Create(TestCase testCase)
        {
            var dirPath = Path.Combine(this.path, testCase.Name);
            var directory = this.fileSystemRepository.CreateDirectory(dirPath);
            testCase.CreatedDateTime = directory.CreatedDateTime;
        }


        public TestCase Get(string testCaseName)
        {
            var dirPath = Path.Combine(this.path, testCaseName);
            var dir = this.fileSystemRepository.GetDirectory(dirPath);
            var testCase = MapTestCase(dir);
            testCase.Definitions = GetTestCaseDefinitions(testCaseName);
            testCase.Executions = GetTestCaseExecutions(testCaseName);

            return testCase;
        }

        private TestCase MapTestCase(Directory directory)
        {
            var testCase = new TestCase();
            testCase.CreatedDateTime = directory.CreatedDateTime;
            testCase.Name = directory.Name;

            return testCase;
        }

        private IEnumerable<TestCaseDefinition> GetTestCaseDefinitions(string testCaseName)
        {
            var result = new List<TestCaseDefinition>();
            var definitionsPath = Path.Combine(this.path, testCaseName, "Definitions");
            var definitionFiles = this.fileSystemRepository.FetchAllFiles(definitionsPath);
            foreach (var definition in definitionFiles)
            {
                var tcd = new TestCaseDefinition();
                tcd.CreatedDateTime = definition.CreatedDateTime;
                tcd.Definition = definition.Contents;
                tcd.Name = Path.GetFileNameWithoutExtension(definition.Name);
                result.Add(tcd);
            }

            return result;
        }

        private IEnumerable<TestCaseExecution> GetTestCaseExecutions(string testCaseName)
        {
            var result = new List<TestCaseExecution>();
            var executionsPath = Path.Combine(this.path, testCaseName, "Executions");
            var executionFiles = this.fileSystemRepository.FetchAllFiles(executionsPath);
            foreach (var execution in executionFiles)
            {
                var tce = TestCaseExecution.FromXml(execution.Contents);
                tce.CreatedDateTime = execution.CreatedDateTime;
                tce.Name = Path.GetFileNameWithoutExtension(execution.Name);
                
                result.Add(tce);
            }

            return result;
        }

        public void AddDefinition(string testCaseName, TestCaseDefinition definition)
        {
            var filePath = Path.Combine(this.path, testCaseName, "Definitions", definition.Name + ".txt");
            var file = fileSystemRepository.CreateFile(filePath, definition.Definition);
            definition.CreatedDateTime = file.CreatedDateTime;
        }

        public void AddExecution(string testCaseName, TestCaseExecution execution)
        {
            var filePath = Path.Combine(this.path, testCaseName, "Executions", execution.Name + ".xml");
            var file = fileSystemRepository.CreateFile(filePath, execution.ToXml());
            execution.CreatedDateTime = file.CreatedDateTime;
        }
    }
}
