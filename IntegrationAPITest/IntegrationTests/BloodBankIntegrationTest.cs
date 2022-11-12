namespace IntegrationAPITest.IntegrationTests
{
    using AutoMapper;
    using IntegrationAPI.Controllers;
    using IntegrationAPI.DTO;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.BloodBank.Interfaces;
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
                                             serviceScope.ServiceProvider.GetRequiredService<IMapper>(), 
                                             serviceScope.ServiceProvider.GetRequiredService<IMailSender>());
        }

        [Fact]
        public void Get_1_ShouldReturnOne()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as GetBloodBankDTO;

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Bank 1");
            result.Email.ShouldBe("zika@hotmail.com");
        }
    }
}
