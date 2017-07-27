using System.Xml.Serialization;

namespace TestSuite.TestManagement
{
    [XmlInclude(typeof(ExecuteMethodStep))]
    [XmlInclude(typeof(FormattingStep))]
    [XmlInclude(typeof(LoadAssemblyStep))]
    [XmlInclude(typeof(SetClassStep))]
    public abstract class TestStep
    {
        public abstract void Accept(ITestStepVisitor visitor);
    }
}