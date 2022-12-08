namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.News;
    using IntegrationAPI.DTO.Notification;
    using IntegrationAPI.DTO.Tender;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.Notification.Enums;
    using IntegrationLibrary.Notification.Interfaces;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Tender.Enums;
    using IntegrationLibrary.Tender.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotificationIntegrationTest : BaseIntegrationTest
    {
        public NotificationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static NotificationController SetupController(IServiceScope serviceScope)
        {
            return new NotificationController(serviceScope.ServiceProvider.GetRequiredService<INotificationService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IMapper>());
        }

        private IntegrationDbContext SetupContext(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<IntegrationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Notifications.Add(NotificationMockData.Notification1);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Get_1_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as NotificationDTO;

            result.ShouldNotBeNull();
            result.BloodUnitStatus.ShouldBe(BloodUnitStatus.IN_STOCK);
            result.BloodType.ShouldBe(BloodType.A_NEGATIVE);
        }

        [Fact]
        public void GetAll_ShouldReturnCount1()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.GetAll()).Value as List<NotificationDTO>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void Create_Notification()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var notification = new NotificationDTO
            {
                BloodUnitStatus = BloodUnitStatus.OUT_OF_STOCK,
                Message = "test",
                BloodType = BloodType.A_POSITIVE
            };
            var result = (StatusCodeResult)controller.Create(notification);

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

    }
}
