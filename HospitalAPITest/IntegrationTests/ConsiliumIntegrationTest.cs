namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Consilium;
    using HospitalAPI.Dto.Examinations;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.Consilium;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConsiliumIntegrationTest : BaseIntegrationTest
    {
        public ConsiliumIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static ConsiliumController SetupController(IServiceScope scope)
        {
            return new ConsiliumController(scope.ServiceProvider.GetRequiredService<IConsiliumService>(),
                scope.ServiceProvider.GetRequiredService<IDoctorScheduleService>());
        }

        [Fact]
        public void Get_by_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as ConsiliumDto;

            Assert.NotNull(result);
            Assert.Equal("Tema", result.Topic);
            Assert.Equal(2, result.Doctors.Count);
            Assert.Equal(30, result.Duration);
            Assert.Contains(result.Doctors, x => x.LastName.Equals("Rapacoti"));
        }

        [Fact]
        public void Schedule_consilium_by_selected_doctors()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            List<int> selectedDoctors = new List<int>();
            selectedDoctors.Add(4);
            selectedDoctors.Add(6);

            ScheduleConsiliumDto dto = new ScheduleConsiliumDto
            {
                DateRange = new DateRange(new DateTime(2022, 12, 17), new DateTime(2022, 12, 22)),
                Topic = "Hitan sastanak oko pacijenta Petra Petrovica.",
                Duration = 30,
                DoctorId = 4,
                SelectedDoctors = selectedDoctors,
                SelectedSpecializations = null,
                RoomId = 1
            };

            var result = ((OkObjectResult)controller.Schedule(dto)).Value as ConsiliumDto;

            Assert.NotNull(result);
            Assert.Equal("Hitan sastanak oko pacijenta Petra Petrovica.", result.Topic);
            Assert.Equal(new DateTime(2022, 12, 21, 7, 30, 0), result.DateTime);
        }

        [Fact]
        public void Schedule_consilium_by_selected_doctors_fail()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            List<int> selectedDoctors = new List<int>();
            selectedDoctors.Add(4);
            selectedDoctors.Add(6);

            ScheduleConsiliumDto dto = new ScheduleConsiliumDto
            {
                DateRange = new DateRange(new DateTime(2022, 12, 17), new DateTime(2022, 12, 19)),
                Topic = "Hitan sastanak oko pacijenta Petra Petrovica.",
                Duration = 30,
                DoctorId = 4,
                SelectedDoctors = selectedDoctors,
                SelectedSpecializations = null,
                RoomId = 1
            };

            var result = ((BadRequestObjectResult)controller.Schedule(dto)).Value;

            Assert.NotNull(result);
            Assert.Equal("One or more selected doctors are not able to attend the consilium in the given period.", result.ToString());
        }

        [Fact]
        public void Test_get_consiliums_for_room()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 2;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<ConsiliumDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_get_consiliums_for_room_empty()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 4;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<ConsiliumDto>;

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Get_all_consiliums_for_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int doctorId = 7;
            var result = ((OkObjectResult)controller.GetAllByDoctorId(doctorId)).Value as List<DisplayConsiliumDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Doctor_has_never_been_to_any_consilium()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int doctorId = 5;

            var result = controller.GetAllByDoctorId(doctorId) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}
