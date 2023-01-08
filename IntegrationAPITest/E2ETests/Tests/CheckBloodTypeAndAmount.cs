﻿namespace IntegrationAPITest.E2ETests.Tests
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
    using OpenQA.Selenium;

    public class CheckBloodTypeAndAmount : IDisposable
    {

        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.BloodBanksPage bloodBanksPage;
        private Pages.BloodBankDetailsPage bloodBankDetailsPage;

        public CheckBloodTypeAndAmount()
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

        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Check_APositive_BloodType_Amount_1()
        {
            
            var bloodbankdetails = new BloodBankDetailsPage(driver);
            bloodbankdetails.EnsurePageIsDisplayed();
            bloodbankdetails.BloodType.Click();
            bloodbankdetails.APositive.Click();
            bloodbankdetails.BloodAmount.SendKeys("1");
            Assert.True(bloodbankdetails.EnsureToastrPopup());

            Dispose();
        }
    }
}