namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPI.EmailServices;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
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
                                             serviceScope.ServiceProvider.GetRequiredService<IDoctorScheduleService>());
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
            int roomId = 5;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<AppointmentDisplayDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public void Get_appointments_for_room_empty()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 4;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<AppointmentDisplayDto>;

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Get_appointments_by_doctor_specialization()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            Specialization spec = Specialization.CARDIOLOGY;
            DateRangeDto dateRangeDto = new DateRangeDto();
            dateRangeDto.From = new DateTime(2023, 1, 8);
            dateRangeDto.To = new DateTime(2023, 1, 18);

            var result = controller.GetAllBySpecialization(spec, dateRangeDto);

            Assert.NotNull(result);

        }

        [Fact]
        public void Reccomend_appointments_by_specialization()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            Specialization spec = Specialization.CARDIOLOGY;
            DateRangeDto dateRangeDto = new DateRangeDto();
            dateRangeDto.From = new DateTime(2023, 1, 8);
            dateRangeDto.To = new DateTime(2023, 1, 15);
            ReccomendBySpecializationRequestDto reccomendRequestDTO = new ReccomendBySpecializationRequestDto()
            {
                DoctorId = 7,
                PatientId = 1,
                DateRange = new DateRange()
                {
                    From = dateRangeDto.From,
                    To = dateRangeDto.To
                }
            };

            var result = ((OkObjectResult)controller.RecommendAppointmentsBySpecialization(reccomendRequestDTO, spec)).Value as IEnumerable<ReccomendedBySpecializationDTO>;

            Assert.NotEmpty(result);
            Assert.Equal(4, result.ElementAt(0).DoctorId);

        }

    }
}
