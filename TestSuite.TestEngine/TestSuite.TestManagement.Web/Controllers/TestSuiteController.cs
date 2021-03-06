﻿using System;
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

        public TestSuiteController() : this(DomainFactory.CreateTestCaseRepository())
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

            var testCases = this.repository.FetchAll();
            model.SetSummary(testCases);

            return View(model);
        }

        [HttpPost]
        [Route("TestCases/Create")]
        public ActionResult CreateTestCase(string name)
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