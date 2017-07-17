using TestSuite.TestManagement.Repositories;

namespace TestSuite.TestManagement
{
    public class DomainFactories
    {
        private IRepositoryFactory repository;
        public IRepositoryFactory Repository
        {
            get
            {
                if (this.repository == null)
                    throw new DomainFactoryException("Domain repository factory not defined. Define a repository factory by setting Domain.Factories.Repository property.");
                return this.repository;
            }
            set { this.repository = value; }
        }

        private ITestStepFactory testStep = new TestStepFactory();
        public ITestStepFactory TestStep
        {
            get
            {
                if (this.testStep == null)
                    throw new DomainFactoryException("Test step factory not defined. Define a test step factory by setting Domain.Factories.TestStep property.");
                return this.testStep;
            }
            set { this.testStep = value; }
        }

        internal DomainFactories() { }
    }
}
