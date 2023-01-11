namespace IntegrationAPITest.E2ETests.Tests
{
    using IntegrationAPITest.E2ETests.Pages;
    using IntegrationAPITest.MockData;
    using IntegrationAPITest.Setup;
    using IntegrationLibrary.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;

    public class ConfigureReport : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.BloodBanksPage bloodBanksPage;
        private Pages.BloodBankDetailsPage bloodBankDetailsPage;

        public ConfigureReport()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");

            driver = new ChromeDriver(options);

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            loginPage.insertEmail("maroko@gmail.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            menuPage = new Pages.MenuPage(driver);
            menuPage.bloodBanksTabClick();

            bloodBanksPage = new Pages.BloodBanksPage(driver);
            bloodBanksPage.EnsurePageIsDisplayed();
            bloodBanksPage.EnsureTableDispley();
            bloodBanksPage.EnsureDataIsFetched();

            bloodBanksPage.Rows.ElementAt(2).Click();

            bloodBankDetailsPage = new Pages.BloodBankDetailsPage(driver);
            bloodBankDetailsPage.ShowConfig.Click();
            bloodBankDetailsPage.EnsureConfigIsDisplayed();

        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        private void Configure_Report()
        {
            bloodBankDetailsPage.Frequent.Clear();
            bloodBankDetailsPage.Frequent.SendKeys("25");
            bloodBankDetailsPage.SaveReport.Click();
            Assert.True(bloodBankDetailsPage.GetToast());
            Dispose();
        }
    }
}
