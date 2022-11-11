namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PatientIntegrationTest : BaseIntegrationTest
    {

        public PatientIntegrationTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {

        }

        private static PatientController SetupController(IServiceScope serviceScope)
        {
            return new PatientController(serviceScope.ServiceProvider.GetRequiredService<IPatientService>());
        }

        [Fact]
        public void Test()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as PatientDto;

            Assert.NotNull(result);
            Assert.Equal("Mika", result.FirstName);
        }

    }
}
