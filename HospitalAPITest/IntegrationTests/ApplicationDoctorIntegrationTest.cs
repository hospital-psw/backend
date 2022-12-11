namespace HospitalAPITest.IntegrationTests
{
    using CsvHelper;
    using HospitalAPI.Controllers;
    using HospitalAPI.Controllers.AppUsers;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctorIntegrationTest : BaseIntegrationTest
    {
        public ApplicationDoctorIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static ApplicationDoctorController SetupController(IServiceScope scope)
        {
            return new ApplicationDoctorController(scope.ServiceProvider.GetRequiredService<IApplicationDoctorService>(), null, null);
        }

        [Fact]
        public void GetAllRecomended()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAllRecomended()).Value as List<ApplicationDoctorDTO>;

            Assert.NotNull(result);
            Assert.Equal("Galina", result[0].FirstName);
            Assert.Equal("Marina", result[1].FirstName);
            Assert.Equal(2, result.Count());

        }
    }
}
