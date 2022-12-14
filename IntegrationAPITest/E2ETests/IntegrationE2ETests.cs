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

            Assert.Equal("http://localhost:4200/changePassword", _chromeDriver.Url);
        }

        [Fact]
        public void Check_APositive_BloodType_Amount_1()
        {
            // login
            using var scope = Factory.Services.CreateScope();
            SetupContext(scope);
            var login = new LoginPage(_chromeDriver);
            var menu = new MenuPage(_chromeDriver);
            var bloodbanks = new BloodBanksPage(_chromeDriver);
            var bloodbankdetails = new BloodBankDetailsPage(_chromeDriver);

            login.Email.SendKeys("maroko@gmail.com");
            login.Password.SendKeys("123.Auth");
            login.SubmitButton.Click();
            Thread.Sleep(3000);

            // move to blood banks tab
            menu.BloodBanksTab.Click();
            Thread.Sleep(3000);

            // move to first blood bank
            bloodbanks.FirstBloodBank.Click();
            Thread.Sleep(3000);

            // fill in form and send
            bloodbankdetails.BloodType.Click();
            bloodbankdetails.APositive.Click();
            bloodbankdetails.BloodAmount.SendKeys("1");
        }

        [Fact]
        public void CheckBloobAmount_PublicApp()
        {
            using var scope = Factory.Services.CreateScope();
            SetupContext(scope);
            var page = new CheckBloodAmountPage(_chromeDriver);

            page.Email.SendKeys("asdasd@sad.cs");
            page.Url.SendKeys("string");
            page.ApiKey.SendKeys("3954ED262A0BA55F204CF62C4A4C8BE7");
            page.BloodType.SendKeys("aPlus");
            page.BloodAmount.SendKeys("4");
            page.CheckButton.Click();

            Assert.Equal("http://localhost:4200/bloodbank/46/detail", _chromeDriver.Url);
        }
    }
}
