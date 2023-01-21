namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EquipmentIntegrationTest : BaseIntegrationTest
    {
        public EquipmentIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }
        private static EquipmentController SetupController(IServiceScope serviceScope)
        {
            return new EquipmentController(serviceScope.ServiceProvider.GetRequiredService<IEquipmentService>());
        }

        [Fact]
        public void Finds_equipment_room()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 3;

            var result = ((OkObjectResult)controller.GetForRoom(roomId)).Value as IEnumerable<EquipmentDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Finds_equipment_room_empty()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 15;

            var result = ((OkObjectResult)controller.GetForRoom(roomId)).Value as IEnumerable<EquipmentDto>; //((OkObjectResult)controller.GetForRoom(roomId));
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
