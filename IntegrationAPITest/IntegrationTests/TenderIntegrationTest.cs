namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.Tender;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Tender.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationLibrary.Tender.Enums;

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

            result.ShouldNotBeNull();
            result.Status.ShouldBe(TenderStatus.OPEN);
            result.DueDate.ShouldBe(DateTime.Now);
        }
    }
}
