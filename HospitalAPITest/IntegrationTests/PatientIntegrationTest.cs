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

    public class PatientIntegrationTest : BaseIntegrationTest
    {

        public PatientIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static ApplicationPatientController SetupController(IServiceScope serviceScope)
        {
            return new ApplicationPatientController(serviceScope.ServiceProvider.GetRequiredService<IApplicationPatientService>(), null, null);
        }

        [Fact]
        public async void Get_patient()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)await controller.Get(1)).Value;
            var dto = result as ApplicationPatientDTO;

            Assert.NotNull(dto);
            Assert.Equal("Mika", dto.FirstName);
        }

    }
}
