using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestSuite.TestExecution
{
    public class TestServer
    {
        public static TestServer Instance { get; private set; } = new TestServer();

        private Queue<ITestCase> queue = new Queue<ITestCase>();

        private TestServer()
        {
        }

        public void Queue(ITestCase testCase)
        {
            if (this.queue.Contains(testCase))
                return;

            if (this.queue.Count == 0)
                this.Execute(testCase);
            else
                this.queue.Enqueue(testCase);
        }

        private void Execute(ITestCase testCase)
        {
        }
    }
}
