using System.Web.Mvc;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.Factories;

namespace TestSuite.TestManagement.Web.Controllers
{
    [RoutePrefix("TestCase")]
    public class TestCaseController : Controller
    {
        private ITestCaseRepository repository;

        public TestCaseController(ITestCaseRepository repository)
        {
            this.repository = repository;
        }

        public TestCaseController() : this(RepositoryFactory.CreateTestCaseRepository())
        {
        }

        [Route("{name}")]
        public ActionResult Index(string name)
        {
            TempData["Error"] = $"Redirect from TestCaseController with name: '{name}'";
            return RedirectToAction("Index", "TestSuite");
        }
    }
}