namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPI.EmailServices;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Service.AppUsers.Core;
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
        public AppointmentIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static AppointmentController SetupController(IServiceScope serviceScope)
        {
            return new AppointmentController(serviceScope.ServiceProvider.GetRequiredService<IAppointmentService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IEmailService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IDoctorScheduleService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IApplicationPatientService>());
        }

        [Fact]
        public void Get_single_appointment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var rec = new RecommendRequestDto()
            {
                Date = DateTime.Now,
                PatientId = 1,
                DoctorId = 3
            };


            var result = ((OkObjectResult)controller.Get(1)).Value as AppointmentDto;

            Assert.NotNull(result);
            Assert.Equal("Djankarlo", result.Doctor.FirstName);
        }

        [Fact]
        public void Get_appointments_for_room()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 4;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<AppointmentDisplayDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public void Get_appointments_for_room_empty()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 5;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<AppointmentDisplayDto>;

            Assert.NotNull(result);
            Assert.Empty(result);
        }

    }
}
