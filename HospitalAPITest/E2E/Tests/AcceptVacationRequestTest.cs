namespace HospitalAPITest.E2E.Tests
{
    using HospitalAPITest.E2E.Pages;
    using NuGet.Frameworks;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AcceptVacationRequestTest
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.DisplayVacationRequestsPage vacationRequestsPage;

        public AcceptVacationRequestTest()
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
            menuPage.managerVacationRequestsTabClick();
            vacationRequestsPage = new Pages.DisplayVacationRequestsPage(driver);
        }

        [Fact]
        public void Test_accept_vacation_request()
        {
            int expanderCountBeforeAccept = vacationRequestsPage.GetExpanderCount();
            vacationRequestsPage.AcceptVacationRequest();
            int expanderCountAfterAccept = vacationRequestsPage.GetExpanderCount();

            Assert.Equal(Pages.DisplayVacationRequestsPage.URI, driver.Url);
            Assert.Equal(expanderCountBeforeAccept - 1, expanderCountAfterAccept);
            Dispose();
        }

        private void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }

    
}
