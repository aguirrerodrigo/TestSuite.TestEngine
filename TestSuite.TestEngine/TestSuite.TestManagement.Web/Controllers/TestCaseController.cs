using System;
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
            var tc = this.GetTestCase(testCase);

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
            var tc = this.GetTestCase(testCase);

            if (!tc.Executions.Any())
                return RedirectToAction(nameof(GetDefinition), new { testCase = testCase });

            var model = new TestCaseViewModel(tc);
            model.SelectResult(resultName);

            return View("Result", model);
        }

        [HttpPost]
        [Route("{testCase}/Result/{resultName}")]
        public ActionResult RunTest(string testCase, string resultName)
        {
            var execution = this.GetTestCaseExecution(testCase, resultName);

            execution.Run(new TestRunner());
            this.repository.UpdateExecution(testCase, execution);

            return RedirectToAction(nameof(GetResult), new { testCase = testCase, resultName = resultName });
        }

        public TestCase GetTestCase(string testCaseName)
        {
            try
            {
                var testCase = this.repository.Get(testCaseName);
                return testCase;
            }
            catch(Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message, ex);
            }
        }

        public TestCaseExecution GetTestCaseExecution(string testCaseName, string testCaseExecutionName)
        {
            try
            {
                var testCase = this.repository.Get(testCaseName);
                var execution = testCase.Executions.FirstOrDefault(e => e.Name == testCaseExecutionName);
                if (execution == null)
                    throw new ResourceNotFoundException($"Could not find execution '{testCaseExecutionName}' for test case '{testCaseName}'.");

                return execution;
            }
            catch(ResourceNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message, ex);
            }
        }
    }
}