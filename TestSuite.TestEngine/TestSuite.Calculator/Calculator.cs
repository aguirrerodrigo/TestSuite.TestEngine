namespace TestSuite.Calculator
{
    public class Calculator
    {
        double currentValue = 0;

        public double GetCurrentValue()
        {
            return currentValue;
        }

        public void Add(double value)
        {
            this.currentValue += value;
        }

        public void Subtract(double value)
        {
            this.currentValue -= value;
        }
    }
}
