namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Microsoft.AspNetCore.Http;

    public class RelocationIntegrationTest : BaseIntegrationTest
    {

        public RelocationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static RelocationController SetupController(IServiceScope serviceScope)
        {
            return new RelocationController(serviceScope.ServiceProvider.GetRequiredService<IRelocationService>(), serviceScope.ServiceProvider.GetRequiredService<IRoomService>(), serviceScope.ServiceProvider.GetRequiredService<IEquipmentService>());
        }

        [Fact]
        public void Test_Create_Relocation_Request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            RelocationRequestDto dto = new RelocationRequestDto(1, 1, 4, 1, new DateTime(2022, 12, 12, 15, 0, 0), 2);
            var result = (OkObjectResult)controller.Create(dto);

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
           
        }
    }
}
