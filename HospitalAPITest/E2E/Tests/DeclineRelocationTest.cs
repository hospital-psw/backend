namespace HospitalAPITest.E2E.Tests
{
    using HospitalAPITest.E2E.Pages;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DeclineRelocationTest
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.DeclineRelocationPage declineRelocationPage;
        private int requestsCount = 0;
        private int newRequestsCount = 0;
        //public const string URI_APPOINTMENTS = "http://localhost:4200/appointments";

        public DeclineRelocationTest()
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
            declineRelocationPage = new Pages.DeclineRelocationPage(driver);
            Assert.True(declineRelocationPage.buildingDisplayed());

        }

        [Fact]
        public void Test()
        {
            ChooseParameters();
            Decline();
            Assert.Equal(requestsCount - 1, newRequestsCount);
            Assert.Equal(Pages.DeclineRelocationPage.URI, driver.Url);
            Dispose();
        }

        [Fact]
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
        private void ChooseParameters()
        {
            declineRelocationPage.SelectBuilding();
            declineRelocationPage.SelectRoom();
            declineRelocationPage.EnsureTabIsDisplayed();
            declineRelocationPage.SelectTab();
            requestsCount = declineRelocationPage.RequestsCount();
        }
        private void Decline()
        {
            declineRelocationPage.Decline();
            newRequestsCount = declineRelocationPage.RequestsCount();
        }
    }
}
