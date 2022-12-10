namespace HospitalAPITest.E2E.Tests
{
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HospitalAPITest.E2E.Pages;

    public class ScheduleRenovationTest
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.ScheduleRenovationPage scheduleRenovationPage;
        public ScheduleRenovationTest()
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
            Assert.True(loginPage.loginButtonDisplayed());
            Assert.True(loginPage.emailInputDisplayed());
            Assert.True(loginPage.passwordInputDisplayed());
            loginPage.insertEmail("maroko@gmail.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            menuPage = new Pages.MenuPage(driver);
            Assert.True(menuPage.managerTabDisplayed());
            menuPage.managerTabClick();
            scheduleRenovationPage = new Pages.ScheduleRenovationPage(driver);
            Assert.True(scheduleRenovationPage.buildingDisplayed());

        }

        [Fact]
        public void Test()
        {
            ChooseParameters();
            Assert.Equal(Pages.ScheduleRenovationPage.URI, driver.Url);
            Dispose();
        }

        private void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        private void ChooseParameters()
        {
            scheduleRenovationPage.SelectBuilding();
            scheduleRenovationPage.SelectFloor();
            scheduleRenovationPage.ClickRenovatioButton();
            scheduleRenovationPage.SelectRenovationType();
            scheduleRenovationPage.SelectFirstRoom();
            scheduleRenovationPage.SelectDates();
            scheduleRenovationPage.EnterDuration();
            scheduleRenovationPage.SelectStartTime();
            scheduleRenovationPage.EnterNewRoomDetails();
  
        }
    }
}
