namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Controllers.AppUsers;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatientIntegrationTest : BaseIntegrationTest
    {
        public ApplicationPatientIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static ApplicationPatientController SetupController(IServiceScope scope)
        {
            return new ApplicationPatientController(scope.ServiceProvider.GetRequiredService<IApplicationPatientService>());
        }
        [Fact]
        public async void Get_app_patient()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)await controller.Get(9)).Value as ApplicationPatientDTO;

            Assert.NotNull(result);
        }
    }
}
