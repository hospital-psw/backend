namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPI.EmailServices;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentIntegrationTest : BaseIntegrationTest
    {
        public AppointmentIntegrationTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static AppointmentController SetupController(IServiceScope serviceScope)
        {
            return new AppointmentController(serviceScope.ServiceProvider.GetRequiredService<IAppointmentService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IEmailService>());
        }

        [Fact]
        public void Test()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var rec = new RecommendRequestDto()
            {
                Date = DateTime.Now,
                PatientId = 1,
                DoctorId = 3
            };


            var result = ((OkObjectResult)controller.Get(4)).Value as AppointmentDto;

            Assert.NotNull(result);
            Assert.Equal("Djankarlo", result.Doctor.FirstName);
        }
    }
}
