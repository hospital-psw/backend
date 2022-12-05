namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;

    public class BloodBankIntegrationTest : BaseIntegrationTest
    {
        public BloodBankIntegrationTest(TestDatabaseFactory factory) : base(factory) { }

        private static BloodBankController SetupController(IServiceScope serviceScope)
        {
            return new BloodBankController(serviceScope.ServiceProvider.GetRequiredService<IBloodBankService>(),
                                             serviceScope.ServiceProvider.GetRequiredService<IMapper>());
        }

        private IntegrationDbContext SetupContext(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<IntegrationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.BloodBanks.Add(BloodBankMockData.BloodBank1);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Get_1_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as GetBloodBankDTO;

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Bank 1");
            result.Email.ShouldBe("zika@hotmail.com");
        }

        [Fact]
        public void Update_Configuration()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);
            var testTime = DateTime.Now;
            var dto = new SaveConfigurationDTO()
            {
                Id = 1,
                Frequently = 80,
                ReportFrom = testTime,
                ReportTo = testTime
            };

            var result = ((OkObjectResult)controller.SaveConfiguration(dto)).Value as GetBloodBankDTO;

            result.ShouldNotBeNull();
            result.Frequently.Equals(80);
            result.ReportFrom.Equals(testTime);
            result.ReportTo.Equals(testTime);
        }
        [Fact]
        public void Add_New_Monthly_Configuration()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            SetupContext(scope);
            var testTime = DateTime.Now;
            var dto = new MonthlyTransfer()
            {
                DateTime = testTime,
                ABMinus = 2,
                ABPlus = 1,
                AMinus = 0,
                APlus = 1,
                BMinus = 2,
                BPlus = 10,
                OMinus = 0,
                OPlus = 0,
            };

            var result = ((OkObjectResult)controller.SaveMonthlyTransferConfiguration(1, dto)).Value as GetBloodBankDTO;

            result.ShouldNotBeNull();
            result.MonthlyTransfer.APlus.Equals(1);
            result.MonthlyTransfer.APlus.Equals(1);
            result.MonthlyTransfer.ABPlus.Equals(1);
            result.MonthlyTransfer.ABMinus.Equals(2);
            result.MonthlyTransfer.OPlus.Equals(0);
        }
    }
}
