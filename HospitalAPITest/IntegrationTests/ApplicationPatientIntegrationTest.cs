namespace HospitalAPITest.IntegrationTests
{
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
        public void Get_app_patient()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(13)).Value as ApplicationPatientDto;

            Assert.NotNull(result);
        }
    }
}
