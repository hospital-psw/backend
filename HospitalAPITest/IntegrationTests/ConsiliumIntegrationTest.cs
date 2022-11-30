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
            Assert.Equal("Topic1", result.Topic);
            Assert.Equal(6, result.Doctors.Count);
            Assert.Equal(30, result.Duration);
        }

        [Fact]
        public void Schedule_consilium_by_selected_doctors()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            List<int> selectedDoctors = new List<int>();
            selectedDoctors.Add(2);
            selectedDoctors.Add(3);
            selectedDoctors.Add(4);

            ScheduleConsiliumDto dto = new ScheduleConsiliumDto {
                DateRange = new DateRange(),
                Topic = "Hitan sastanak oko pacijenta Petra Petrovica.",
                Duration = 30,
                DoctorId = 3,
                SelectedDoctors = selectedDoctors,
                SelectedSpecializations = null
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
            selectedSpecializations.Add(Specialization.NEUROLOGY);

            ScheduleConsiliumDto dto = new ScheduleConsiliumDto
            {
                DateRange = new DateRange(),
                Topic = "Hitan sastanak oko pacijenta Petra Petrovica.",
                Duration = 30,
                DoctorId = 3,
                SelectedDoctors = null,
                SelectedSpecializations = selectedSpecializations
            };

            var result = ((OkObjectResult)controller.Schedule(dto)).Value as ConsiliumDto;

            Assert.NotNull(result);
            Assert.Equal("Hitan sastanak oko pacijenta Petra Petrovica.", result.Topic);
        }
    }
}
