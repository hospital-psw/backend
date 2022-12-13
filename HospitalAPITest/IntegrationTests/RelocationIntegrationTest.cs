namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class RelocationIntegrationTest : BaseIntegrationTest
    {

        public RelocationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static RelocationController SetupController(IServiceScope serviceScope)
        {
            return new RelocationController(serviceScope.ServiceProvider.GetRequiredService<IRelocationService>(), serviceScope.ServiceProvider.GetRequiredService<IRoomService>(), serviceScope.ServiceProvider.GetRequiredService<IEquipmentService>(), serviceScope.ServiceProvider.GetRequiredService<IRoomScheduleService>());
        }

        [Fact]
        public void Test_Create_Relocation_Request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            RelocationRequestDto dto = new RelocationRequestDto(1, 2, 4, 1, new DateTime(2023, 2, 20, 15, 0, 0), 2);
            var result = (OkObjectResult)controller.Create(dto);

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

        }

        [Fact]
        public void Test_get_relocations_for_room()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 4;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<RelocationRequestDisplayDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_get_relocations_for_room_empty()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 1;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<RelocationRequestDisplayDto>;

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Test_decline_relocation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.Decline(1) as StatusCodeResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);


        }
    }
}
