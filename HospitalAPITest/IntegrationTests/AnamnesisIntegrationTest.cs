namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers.Examinations;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnamnesisIntegrationTest : BaseIntegrationTest
    {
        public AnamnesisIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        public AnamnesisController SetupController(IServiceScope scope)
        {
            return new AnamnesisController(scope.ServiceProvider.GetRequiredService<IAnamnesisService>());
        }


    }
}
