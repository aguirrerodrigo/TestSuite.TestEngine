using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.TestExecution
{
    public class TestExecution
    {
        private ITestCase testCase;

        public TestExecution(ITestCase testCase)
        {
            this.testCase = testCase;
        }

        public void Execute()
        {

        }
    }
}
