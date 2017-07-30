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

        [Route("{testCase}")]
        public ActionResult Index(string testCase)
        {
            return GetResult(testCase, null);
        }

        [Route("{testCase}/Definition/{definitionName?}")]
        public ActionResult GetDefinition(string testCase, string definitionName = null)
        {
            var tc = this.repository.Get(testCase);

            if (string.IsNullOrWhiteSpace(definitionName) && tc.Definitions.Any())
                definitionName = tc.Definitions.First().Name;

            var model = new TestCaseViewModel(tc);
            model.SelectDefinition(definitionName);

            return View("Definition", model);
        }

        [HttpPost]
        [Route("{testCase}/Definition")]
        [ValidateInput(false)]
        public ActionResult UpdateDefinition(string testCase, string definition)
        {
            var tc = new TestCase();
            tc.Name = testCase;
            tc.AddDefinition(definition, this.repository);
            tc.AddExecution(definition, this.repository);

            return RedirectToAction(nameof(Index), new { testCase = testCase });
        }
        
        [Route("{testCase}/Result/{resultName?}")]
        public ActionResult GetResult(string testCase, string resultName = null)
        {
            var tc = this.repository.Get(testCase);

            if (!tc.Executions.Any())
                return RedirectToAction(nameof(GetDefinition), new { testCase = testCase });

            if (string.IsNullOrWhiteSpace(resultName))
                resultName = tc.Executions.First().Name;

            var model = new TestCaseViewModel(tc);
            model.SelectResult(resultName);

            return View("Result", model);
        }
    }
}