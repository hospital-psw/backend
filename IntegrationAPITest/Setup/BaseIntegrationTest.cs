namespace IntegrationAPITest.Setup
{
    using IntegrationAPI;

    public class BaseIntegrationTest : IClassFixture<TestDatabaseFactory>
    {
        protected TestDatabaseFactory Factory { get; }

        public BaseIntegrationTest(TestDatabaseFactory factory)
        {
            Factory = factory;
        }
    }
}
