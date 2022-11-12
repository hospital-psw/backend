namespace HospitalAPITest.Setup
{
    using HospitalAPI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseIntegrationTest : IClassFixture<TestDatabaseFactory>
    {
        protected TestDatabaseFactory Factory { get; }

        public BaseIntegrationTest(TestDatabaseFactory factory)
        {
            Factory = factory;
        }
    }
}
