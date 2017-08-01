using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite.Calculator.Test
{
    [TestClass]
    public class CalculatorTest
    {
        private Calculator calculator = new Calculator();

        [TestMethod]
        public void Test()
        {
            this.TestCurrentValueShouldEqual("0");
            this.Add("100");
            this.Add("-50");
            this.TestCurrentValueShouldEqual("50");
            this.Subtract("200");
            this.Subtract("-250");
            this.TestCurrentValueShouldEqual("100");
        }

        public void Add(string value)
        {
            calculator.Add(Convert.ToDouble(value));
        }

        public void Subtract(string value)
        {
            calculator.Subtract(Convert.ToDouble(value));
        }

        public void TestCurrentValueShouldEqual(string value)
        {
            Assert.AreEqual(Convert.ToDouble(value), calculator.GetCurrentValue());
        }


    }
}
