namespace IntegrationAPITest.E2ETests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationAPITest.E2ETests.Pages;
    using IntegrationAPITest.MockData;
    using IntegrationLibrary.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager.DriverConfigs.Impl;
    using WebDriverManager;
    using IntegrationAPITest.Setup;

    public class CheckBloodTypeAndAmount : BaseIntegrationTest
    {

        private ChromeDriver _chromeDriver;
        public CheckBloodTypeAndAmount(TestDatabaseFactory factory) : base(factory)
        {
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            _chromeDriver = new ChromeDriver();

            using var scope = Factory.Services.CreateScope();
            SetupContext(scope);
            
            var login = new LoginPage(_chromeDriver);
            login.EnsurePageIsDisplayed();
            login.Email.SendKeys("maroko@gmail.com");
            login.Password.SendKeys("123.Auth");
            login.SubmitButton.Click();
            login.WaitToRedirectToMenuPage();

            var menu = new MenuPage(_chromeDriver);
            menu.EnsurePageIsDisplayed();
            menu.BloodBanksTab.Click();
            menu.WaitToRedirectToBloodBanksPage();

            var bloodbanks = new BloodBanksPage(_chromeDriver);
            bloodbanks.EnsurePageIsDisplayed();
            bloodbanks.FirstBloodBank.Click();
            bloodbanks.WaitToRedirectToBBDetailsPage();
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

        private void Dispose()
        {
            _chromeDriver.Quit();
            _chromeDriver.Dispose();
        }

        [Fact]
        public void Check_APositive_BloodType_Amount_1()
        {
            using var scope = Factory.Services.CreateScope();
            SetupContext(scope);
            
            var bloodbankdetails = new BloodBankDetailsPage(_chromeDriver);
            bloodbankdetails.EnsurePageIsDisplayed();
            bloodbankdetails.BloodType.Click();
            bloodbankdetails.APositive.Click();
            bloodbankdetails.BloodAmount.SendKeys("1");
            Assert.True(bloodbankdetails.EnsureToastrPopup());

            Dispose();
        }
    }
}
