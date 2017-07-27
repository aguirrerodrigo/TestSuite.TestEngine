using System.Collections.Generic;

namespace TestSuite.TestManagement
{
    public class TestStepCollection : List<TestStep>
    {
        public void Accept(ITestStepVisitor visitor)
        {
            foreach(var elem in this)
            {
                elem.Accept(visitor);
            }
        }
    }
}
