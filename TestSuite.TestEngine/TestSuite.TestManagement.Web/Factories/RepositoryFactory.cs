using System.Configuration;
using System.IO;
using System.Web;
using TestSuite.TestManagement.FileSystemRepository;
using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement.Web.Factories
{
    public static class RepositoryFactory
    {
        public static ITestCaseRepository CreateTestCaseRepository()
        {
            var dirPath = MapRepositoryPath("TestCase");
            var result = new TestCaseRepository(dirPath);
            return result;
        }

        private static string MapRepositoryPath(string directoryName)
        {
            var serverPath = HttpContext.Current.Server.MapPath("~");
            var repositoryPath = ConfigurationManager.AppSettings["FileSystemRepository.Root"] ?? "Repository";
            var dirName = "TestCase";
            var dirPath = Path.Combine(serverPath, repositoryPath, dirName);

            return dirPath;
        }
    }
}