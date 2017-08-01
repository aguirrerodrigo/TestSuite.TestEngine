using System;
using System.IO;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class TestRunner : ITestRunner, ITestStepVisitor
    {
        private string assembliesPath;
        private TestEngine.TestEngine testEngine;
        private bool hasError = false;

        public TestRunner(string assembliesPath)
        {
            this.assembliesPath = assembliesPath;
        }

        public void Run(TestCaseExecution testCaseExecution)
        {
            hasError = false;
            testEngine = new TestEngine.TestEngine();

            testCaseExecution.Started = DateTime.Now;
            testCaseExecution.Status = ExecutionStatus.InProgress;

            testCaseExecution.Steps.Accept(this);

            testCaseExecution.Status = hasError ? ExecutionStatus.Failed : ExecutionStatus.Passed;
            testCaseExecution.Ended = DateTime.Now;
        }

        public void Visit(ExecuteMethodStep executeMethodStep)
        {
            try
            {
                executeMethodStep.Status = ExecutionStatus.InProgress;
                var method = new TestEngine.Method(executeMethodStep.MethodName);
                foreach (var p in executeMethodStep.Parameters)
                    method.Parameters[p.Name] = p.Value;

                testEngine.MethodExecution.Execute(method);
                executeMethodStep.Status = ExecutionStatus.Passed;
                executeMethodStep.Error = null;
            }
            catch(Exception ex)
            {
                executeMethodStep.Status = ExecutionStatus.Failed;
                executeMethodStep.Error = ex.Message;
                hasError = true;
            }
        }

        public void Visit(FormattingStep formattingStep)
        {
        }

        public void Visit(LoadAssemblyStep loadAssemblyStep)
        {
            try
            {
                loadAssemblyStep.Status = ExecutionStatus.InProgress;
                var assemblyPath = Path.Combine(assembliesPath, loadAssemblyStep.AssemblyPath);
                testEngine.LoadAssembly(assemblyPath);
                loadAssemblyStep.Status = ExecutionStatus.Passed;
                loadAssemblyStep.Error = null;
            }
            catch (Exception ex)
            {
                loadAssemblyStep.Status = ExecutionStatus.Failed;
                loadAssemblyStep.Error = ex.Message;
                hasError = true;
            }
        }

        public void Visit(SetClassStep setClassStep)
        {
            try
            {
                setClassStep.Status = ExecutionStatus.InProgress;
                testEngine.SetClass(setClassStep.QualifiedName);
                setClassStep.Status = ExecutionStatus.Passed;
                setClassStep.Error = null;
            }
            catch (Exception ex)
            {
                setClassStep.Status = ExecutionStatus.Failed;
                setClassStep.Error = ex.Message;
                hasError = true;
            }
        }
    }
}