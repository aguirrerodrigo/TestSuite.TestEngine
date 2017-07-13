using TestSuite.TestEngine.MockReference;

namespace TestSuite.TestEngine.Mock
{
    public class ClassWithReference
    {
        private ReferenceClass reference;

        public ClassWithReference()
        {
            reference = new ReferenceClass();
        }

        public void Method1()
        {
        }
    }
}
