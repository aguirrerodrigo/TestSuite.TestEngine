namespace TestSuite.TestManagement
{
    public interface ITestStepVisitor
    {
        void Visit(ExecuteMethodStep executeMethodStep);
        void Visit(FormattingStep formattingStep);
        void Visit(LoadAssemblyStep loadAssemblyStep);
        void Visit(SetClassStep setClassStep);
    }
}