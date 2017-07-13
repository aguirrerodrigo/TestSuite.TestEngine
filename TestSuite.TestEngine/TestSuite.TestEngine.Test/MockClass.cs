namespace TestSuite.TestEngine.Test
{
    public class MockClass
    {
        public static bool method1Executed = false;
        public static bool method2Executed = false;
        public static bool method3Executed = false;

        public void Method1(string param1, int param2)
        {
            method1Executed = true;
        }

        public void Method2(string param1, object param2)
        {
            method2Executed = true;
        }

        public void Method3()
        {
            method3Executed = true;
        }
    }
}
