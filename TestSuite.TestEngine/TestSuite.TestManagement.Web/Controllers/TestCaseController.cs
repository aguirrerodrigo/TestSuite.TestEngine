using System;
using System.Web.Mvc;
using TestSuite.TestManagement.Repositories;
using TestSuite.TestManagement.Web.Factories;
using TestSuite.TestManagement.Web.ViewModels;

namespace TestSuite.TestManagement.Web.Controllers
{
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

        public ActionResult Index()
        {
            var model = TempData["Model"] as TestCaseViewModel;
            if (model == null)
                model = new TestCaseViewModel();

            var error = TempData["Error"] as string;
            if (error != null)
                ModelState.AddModelError(string.Empty, error);

            model.TestCases = this.repository.FetchAll();

            return View(model);
        }

        private ActionResult Index(TestCaseViewModel testCase, string error)
        {
            return View(testCase);
        }

        [HttpPost]
        public ActionResult Create(string name)
        {
            try
            {
                var testCase = new TestCase();
                testCase.Name = name;
                testCase.Create(this.repository);
            }
            catch(Exception ex)
            {
                TempData["Model"] = new TestCaseViewModel(name);
                TempData["Error"] = $"Could not create test case. {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}