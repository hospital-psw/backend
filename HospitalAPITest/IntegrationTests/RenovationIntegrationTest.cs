﻿namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class RenovationIntegrationTest : BaseIntegrationTest
    {

        public RenovationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static RenovationController SetupController(IServiceScope serviceScope)
        {
            return new RenovationController(serviceScope.ServiceProvider.GetRequiredService<IRenovationService>(), serviceScope.ServiceProvider.GetRequiredService<IRoomService>(), serviceScope.ServiceProvider.GetRequiredService<IRoomScheduleService>());
        }

        [Fact]
        public void Test_Create_Merge_Renovation_Request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var renovationDetails = new List<RenovationDetailsDto>() { new RenovationDetailsDto("newR1", "ordinacija1", 5) };
            List<int> rooms = new() { 1, 2 };
            RenovationRequestDto dto = new(RenovationType.MERGE, rooms, new DateTime(2023, 1, 20, 15, 0, 0), 4, renovationDetails);

            var result = (OkObjectResult)controller.Create(dto);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Test_Create_Split_Renovation_Request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var renovationDetails = new List<RenovationDetailsDto>() { new RenovationDetailsDto("newR1", "ordinacija1", 5), new RenovationDetailsDto("newR2", "ordinacija2", 10) };
            List<int> rooms = new() { 1 };
            RenovationRequestDto dto = new(RenovationType.SPLIT, rooms, new DateTime(2023, 1, 20, 15, 0, 0), 4, renovationDetails);

            var result = (OkObjectResult)controller.Create(dto);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Test_get_renovations_for_room()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 2;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<RenovationRequestDisplayDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_get_renovations_for_room_empty()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            int roomId = 3;

            var result = ((OkObjectResult)controller.GetAllForRoom(roomId)).Value as IEnumerable<RenovationRequestDisplayDto>;

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Test_decline_renovation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.Decline(1) as StatusCodeResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);


        }
    }
}
