namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequestsIntegrationTest : BaseIntegrationTest
    {
        public VacationRequestsIntegrationTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {

        }

        private static VacationRequestsController SetupController(IServiceScope serviceScope)
        {
            return new VacationRequestsController(serviceScope.ServiceProvider.GetRequiredService<IVacationRequestsService>());
        }

        [Fact]
        public void Test()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAll()).Value as List<VacationRequestDto>;

            Assert.NotNull(result);
        }
    }
}
