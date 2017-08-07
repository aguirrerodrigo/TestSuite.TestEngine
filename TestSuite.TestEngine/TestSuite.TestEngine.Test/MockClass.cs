using System;
using System.IO;

namespace TestSuite.TestEngine.Test
{
    public class MockClass
    {
        public static bool methodAccessingStaticExecuted = false;
        public bool methodWithNoParametersExecuted = false;
        public bool methodWithParametersExecuted = false;
        public string exceptionMessage;

        public void MethodWithNoParameters()
        {
            methodWithNoParametersExecuted = true;
        }

        public void MethodWithParameters(string param1, string param2)
        {
            methodWithParametersExecuted = true;
        }

        public void MethodThrowsException()
        {
            throw new NotImplementedException(exceptionMessage);
        }

        public void WriteToFile(string file, string contents)
        {
            File.WriteAllText(file, contents);
        }

        public void MethodAccessingStatic()
        {
            methodAccessingStaticExecuted = true;
        }
    }
}
