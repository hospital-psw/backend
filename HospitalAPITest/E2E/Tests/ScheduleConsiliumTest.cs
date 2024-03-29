﻿namespace HospitalAPITest.E2E.Tests
{
    using HospitalAPITest.E2E.Pages;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScheduleConsiliumTest : IDisposable
    {
        private readonly IWebDriver driver;
        private ScheduleConisliumPage scheduleConisliumPage;
        private ConsiliumsPage consiliumsPage;
        private LoginPage loginPage;
        private MenuDoctorPage menuDoctorPage;
        private const string consiliumsURI = "http://localhost:4200/app/consiliums";

        public ScheduleConsiliumTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");


            driver = new ChromeDriver(options);

            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.loginButtonDisplayed());
            Assert.True(loginPage.emailInputDisplayed());
            Assert.True(loginPage.passwordInputDisplayed());
            loginPage.insertEmail("ckepa@gmail.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitToRedirectToDoctorsApp();

            menuDoctorPage = new MenuDoctorPage(driver);
            menuDoctorPage.EnsurePageIsDisplayed();
            menuDoctorPage.consiliumTabClick();

            consiliumsPage = new ConsiliumsPage(driver);
            consiliumsPage.EnsurePageIsDisplayed();
            Assert.True(consiliumsPage.ButtonDisplayed());
            consiliumsPage.ButtonClick();

            scheduleConisliumPage = new ScheduleConisliumPage(driver);
            scheduleConisliumPage.EnsurePageIsDisplayed();
            Assert.Equal(ScheduleConisliumPage.URI, driver.Url);
            Assert.True(scheduleConisliumPage.RoomSelectionDisplayed());
            Assert.True(scheduleConisliumPage.TextAreaDisplayed());
            Assert.True(scheduleConisliumPage.ScheduleButtonDisplayed());

        }

        [Fact]
        public void ScheduleConisliumWithSelectedDoctors()
        {
            FillScheduleForm();
            SelectDoctorListElements();
            scheduleConisliumPage.Submit();
            consiliumsPage.EnsurePageIsDisplayed();
            Assert.Equal(consiliumsURI, driver.Url);
            Assert.True(consiliumsPage.ButtonDisplayed());
            Dispose();
        }

        [Fact]
        public void ScheduleConisliumWithSelectedSpecializations()
        {
            FillScheduleForm();
            SelectSpecializationListElements();
            scheduleConisliumPage.Submit();
            consiliumsPage.EnsurePageIsDisplayed();
            Assert.Equal(consiliumsURI, driver.Url);
            Assert.True(consiliumsPage.ButtonDisplayed());
            Dispose();
        }

        [Fact]
        public void FailToScheduleConsilium()
        {
            FailToFillScheduleForm();
            SelectDoctorListElements();
            scheduleConisliumPage.Submit();
            scheduleConisliumPage.EnsurePageIsDisplayed();
            Assert.NotEqual(consiliumsURI, driver.Url);
            Dispose();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        private void FillScheduleForm()
        {
            scheduleConisliumPage.SelectRoom();
            scheduleConisliumPage.SelectDateRange();
            scheduleConisliumPage.TypeTopic();
        }

        private void FailToFillScheduleForm()
        {
            scheduleConisliumPage.SelectDateRange();
            scheduleConisliumPage.TypeTopic();
        }

        private void SelectDoctorListElements()
        {
            scheduleConisliumPage.SelectDoctors();
        }

        private void SelectSpecializationListElements()
        {
            scheduleConisliumPage.SelectSpecializations();
        }

    }
}
