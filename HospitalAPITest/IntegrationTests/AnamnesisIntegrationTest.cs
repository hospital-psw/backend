namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers.Examinations;
    using HospitalAPI.Dto.Examinations;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Mvc;
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
            return new AnamnesisController(scope.ServiceProvider.GetRequiredService<IAnamnesisService>(), scope.ServiceProvider.GetRequiredService<IPrescriptionService>());
        }

        [Fact]
        public void Gets_by_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as AnamnesisDto;

            Assert.NotNull(result);
            Assert.Equal(1, result.Appointment.Id);
            Assert.Equal("Totalna blejica", result.Description);
        }

    }
}
