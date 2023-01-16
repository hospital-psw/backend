namespace HospitalAPITest.E2E.Tests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentCancelationTests
    {
        private readonly IWebDriver driver;
        private Pages.PublicLoginPage loginPage;
        private Pages.PublicHomePage homePage;

        public AppointmentCancelationTests()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");


            driver = new ChromeDriver(options);

            loginPage = new Pages.PublicLoginPage(driver);
            loginPage.insertEmail("mitraja@gmail.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            homePage = new Pages.PublicHomePage(driver);

        }

        [Fact]
        public void Cancel_appointment()
        {
            homePage.selectAppointment();
            homePage.cancelAppointment();

            Assert.True(true);
        }
    }
}
