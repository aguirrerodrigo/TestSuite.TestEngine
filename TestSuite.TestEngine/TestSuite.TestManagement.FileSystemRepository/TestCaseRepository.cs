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
        
        public void AddDefinition(string testCaseName, TestCaseDefinition definition)
        {
            var filePath = Path.Combine(this.path, testCaseName, "Definitions", definition.Name + ".txt");
            var file = fileSystemRepository.CreateFile(filePath, definition.Definition);
            definition.CreatedDateTime = file.CreatedDateTime;
        }

        public void Create(TestCase testCase)
        {
            var dirPath = Path.Combine(this.path, testCase.Name);
            var directory = this.fileSystemRepository.CreateDirectory(dirPath);
            testCase.CreatedDateTime = directory.CreatedDateTime;
        }

        public IEnumerable<TestCase> FetchAll()
        {
            List<TestCase> result = new List<TestCase>();

            var directories = this.fileSystemRepository.FetchAllDirectories(this.path).OrderByDescending(d => d.CreatedDateTime);
            foreach(var directory in directories)
            {
                var testCase = MapTestCase(directory);
                result.Add(testCase);
            }

            return result;
        }

        public TestCase Get(string testCaseName)
        {
            var dirPath = Path.Combine(this.path, testCaseName);
            var dir = this.fileSystemRepository.GetDirectory(dirPath);
            var testCase = MapTestCase(dir);
            testCase.Definitions = GetTestCaseDefinitions(testCaseName);

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

        private TestCase MapTestCase(Directory directory)
        {
            var testCase = new TestCase();
            testCase.CreatedDateTime = directory.CreatedDateTime;
            testCase.Name = directory.Name;

            return testCase;
        }
    }
}
