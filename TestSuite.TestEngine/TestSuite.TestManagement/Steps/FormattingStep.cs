namespace TestSuite.TestManagement
{
    public class FormattingStep : TestStep
    {
        public string FormattingText { get; set; }

        public override void Accept(ITestStepVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}