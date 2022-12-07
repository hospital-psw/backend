namespace IntegrationAPITest.E2ETests
{
    using IntegrationAPITest.E2ETests.Pages;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;

    public class IntegrationE2ETests : BaseIntegrationTest
    {
        private ChromeDriver _chromeDriver;
        public IntegrationE2ETests(TestDatabaseFactory factory) : base(factory) 
        {
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            _chromeDriver = new ChromeDriver();
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
        public void RegisterBloodBank_PublicApp()
        {
            using var scope = Factory.Services.CreateScope();
            SetupContext(scope);
            var page = new RegisterPage(_chromeDriver);

            page.Email.SendKeys("zika@hotmail.com");
            page.Password.SendKeys("4321");
            page.SubmitButton.Click();

            Assert.Equal(_chromeDriver.Url, "http://localhost:4200/changePassword");
        }
    }
}
