namespace HospitalAPITest.IntegrationTests
{
    using CsvHelper;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model.Blood.Enums;
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
            return new ApplicationDoctorController(scope.ServiceProvider.GetRequiredService<IApplicationDoctorService>());
        }
        [Fact]
        public void GetAll()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAll()).Value as List<ApplicationDoctorDto>;

            Assert.NotNull(result);
            Assert.Equal("Galina", result.First().FirstName);

        }
    }
}
