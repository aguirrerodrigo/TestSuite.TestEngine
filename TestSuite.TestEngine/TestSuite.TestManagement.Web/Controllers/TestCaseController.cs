using System.Linq;
using System.Web.Mvc;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.Factories;
using TestSuite.TestManagement.Web.ViewModels;

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

        [Route("{testCase}/{definitionName?}")]
        public ActionResult Index(string testCase, string definitionName)
        {
            TestCase tc = this.repository.Get(testCase);

            if (string.IsNullOrEmpty(definitionName) && tc.Definitions.Any())
                definitionName = tc.Definitions.First().Name;

            var model = new TestCaseViewModel(tc, definitionName);
            return View(model);
        }

        [HttpPost]
        [Route("TestCases/{testCase}/Update")]
        [ValidateInput(false)]
        public ActionResult UpdateDefinition(string testCase, string definition)
        {
            TestCase tc = this.repository.Get(testCase);
            tc.UpdateDefinition(definition, this.repository);

            return RedirectToAction("Index", new { testCase = testCase });
        }
    }
}