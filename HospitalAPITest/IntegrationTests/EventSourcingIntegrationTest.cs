namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Enum;
    using HospitalAPI.Dto.MedicamentTreatment;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventSourcingIntegrationTest : BaseIntegrationTest
    {
        public EventSourcingIntegrationTest(TestDatabaseFactory factory) : base(factory) { }
        private static EventController SetupController(IServiceScope scope)
        {
            return new EventController(scope.ServiceProvider.GetRequiredService<IRenovationEventService>());
        }

        [Fact]
        public void Create_Renovation_Event()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            RenovationEventDto dto = new RenovationEventDto(1, RenovationEventType.PREVIOUS_EVENT_1, DateTime.Now, RenovationType.MERGE);

            var result = ((OkObjectResult)controller.CreateRenovationEvent(dto)).Value;

            Assert.NotNull(result);
            Assert.Equal(1, result);
        }

    }
}
