using System;
using System.Web.Mvc;
using TestSuite.TestManagement.Repositories;
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

        public TestCaseController() : this(new MockTestCaseRepository())
        {
        }

        public ActionResult Index()
        {
            return View();
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
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index", new TestCaseViewModel(name));
            }

            return RedirectToAction("Index");
        }
    }

    public class MockTestCaseRepository : ITestCaseRepository
    {
        public void AddDefinition(string testCase, TestCaseDefinition definition)
        {
            throw new NotImplementedException();
        }

        public void Create(TestCase testCase)
        {
            throw new NotImplementedException();
        }
    }
}