using System;
using System.Linq;
using System.Web.Mvc;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.Factories;
using TestSuite.TestManagement.Web.ViewModels;

namespace TestSuite.TestManagement.Web.Controllers
{
    public class TestSuiteController : Controller
    {
        private ITestCaseRepository repository;

        public TestSuiteController(ITestCaseRepository repository)
        {
            this.repository = repository;
        }

        public TestSuiteController() : this(RepositoryFactory.CreateTestCaseRepository())
        {
        }

        [Route("TestSuite")]
        public ActionResult Index()
        {
            var model = TempData["Model"] as TestSuiteViewModel;
            if (model == null)
                model = new TestSuiteViewModel();

            var error = TempData["Error"] as string;
            if (error != null)
                ModelState.AddModelError(string.Empty, error);

            model.TestCases = this.repository.FetchAll()
                .Select(tc => new TestCaseViewModel(tc));

            return View(model);
        }

        [HttpPost]
        [Route("TestCases/Create")]
        public ActionResult CreateTestCase([Bind(Prefix = "TestCase")]string name)
        {
            try
            {
                var testCase = new TestCase();
                testCase.Name = name;
                testCase.Create(this.repository);
            }
            catch (Exception ex)
            {
                TempData["Model"] = new TestSuiteViewModel(name);
                TempData["Error"] = $"Could not create test case. {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}