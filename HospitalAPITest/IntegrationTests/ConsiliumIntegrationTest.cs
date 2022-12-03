namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.Consilium;
    using HospitalAPI.Dto.Examinations;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.Consilium;
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
            selectedDoctors.Add(1);
            selectedDoctors.Add(2);

            ScheduleConsiliumDto dto = new ScheduleConsiliumDto
            {
                DateRange = new DateRange(new DateTime(2022, 12, 28), new DateTime(2022, 12, 31)),
                Topic = "Hitan sastanak oko pacijenta Petra Petrovica.",
                Duration = 30,
                DoctorId = 1,
                SelectedDoctors = selectedDoctors,
                SelectedSpecializations = null,
                RoomId = 1
            };

            var result = ((OkObjectResult)controller.Schedule(dto)).Value as ConsiliumDto;

            Assert.NotNull(result);
            Assert.Equal("Hitan sastanak oko pacijenta Petra Petrovica.", result.Topic);
        }

        [Fact]
        public void Schedule_consilium_by_selected_specializations()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            List<Specialization> selectedSpecializations = new List<Specialization>();
            selectedSpecializations.Add(Specialization.GENERAL);
            selectedSpecializations.Add(Specialization.CARDIOLOGY);

            ScheduleConsiliumDto dto = new ScheduleConsiliumDto
            {
                DateRange = new DateRange(new DateTime(2022, 12, 28), new DateTime(2022, 12, 31)),
                Topic = "Hitan sastanak oko pacijenta Petra Petrovica.",
                Duration = 30,
                DoctorId = 1,
                SelectedDoctors = null,
                SelectedSpecializations = selectedSpecializations,
                RoomId = 1
            };

            var result = ((OkObjectResult)controller.Schedule(dto)).Value as ConsiliumDto;

            Assert.NotNull(result);
            Assert.Equal("Hitan sastanak oko pacijenta Petra Petrovica.", result.Topic);
        }
    }
}
