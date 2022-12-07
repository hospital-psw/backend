namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.Notification;
    using IntegrationAPI.DTO.Tender;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Notification.Enums;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Tender;
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

    public class TenderIntegrationTest : BaseIntegrationTest
    {
        public TenderIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static TenderController SetupController(IServiceScope serviceScope)
        {
            return new TenderController(serviceScope.ServiceProvider.GetRequiredService<ITenderService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IMapper>());
        }

        private IntegrationDbContext SetupContext(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<IntegrationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Tenders.Add(TenderMockData.Tender1);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Get_1_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as GetTenderDTO;

            DateTime dateTime = new DateTime(2022, 12, 01);

            result.ShouldNotBeNull();
            result.Status.ShouldBe(TenderStatus.OPEN);
            result.DueDate.ShouldBe(dateTime);
        }

        [Fact]
        public void Create_Tender()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var date = new DateTime(2022, 12, 12);
            var money = new Money();
            var item = new TenderItem()
            {
                BloodType = BloodType.A_NEGATIVE,
                Money = new Money()
                {
                    Amount = 10.2,
                    Currency = Currency.EUR
                },
                Quantity = 1
            };
            var items = new List<TenderItem>();
            items.Add(item);
            var tender = new CreateTenderDTO
            {
                DueDate = date,
                Items = items
            };
            var result = (StatusCodeResult)controller.Create(tender);

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
