using System.Configuration;
using System.IO;
using System.Web;
using TestSuite.TestManagement.FileSystemRepository;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.ViewModels;

namespace TestSuite.TestManagement.Web.Factories
{
    public static class DomainFactory
    {
        public static ITestCaseRepository CreateTestCaseRepository()
        {
            var dirPath = MapRepositoryPath("TestCase");
            var result = new TestCaseRepository(dirPath);

            return result;
        }

        public static ITestRunner CreateTestRunner()
        {
            var path = MapAssembliesPath();
            var result = new TestRunner(path);

            return result;
        }

        private static string MapRepositoryPath(string directoryName)
        {
            var repositoryPath = ConfigurationManager.AppSettings["FileSystemRepository.Root"];
            var serverPath = HttpContext.Current.Server.MapPath("~");
            var result = Path.Combine(serverPath, repositoryPath, directoryName);

            return result;
        }

        private static string MapAssembliesPath()
        {
            var assembliesPath = ConfigurationManager.AppSettings["Assemblies.Root"];
            var serverPath = HttpContext.Current.Server.MapPath("~");
            var result = Path.Combine(serverPath, assembliesPath);
         
            return result;
        }
    }
}