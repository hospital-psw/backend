namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class PatientIntegrationTest : BaseIntegrationTest
    {

        public PatientIntegrationTest(TestDatabaseFactory factory) : base(factory)
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
