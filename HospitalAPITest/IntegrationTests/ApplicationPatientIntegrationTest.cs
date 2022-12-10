namespace HospitalAPITest.IntegrationTests
{
    using AutoMapper;
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
            return new ApplicationPatientController(scope.ServiceProvider.GetRequiredService<IApplicationPatientService>(), scope.ServiceProvider.GetRequiredService<IAuthService>(), scope.ServiceProvider.GetRequiredService<IMapper>());
        }
        [Fact]
        public void Get_blocked_app_patients()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetBlocked()).Value as List<ApplicationPatientDTO>;

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Get_malicious_app_patients()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetMalicious()).Value as List<ApplicationPatientDTO>;

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
