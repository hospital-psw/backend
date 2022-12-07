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
        private Pages.DeclineRelocationPage declineRelocationPage;
        private int requestsCount = 0;
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


            declineRelocationPage = new Pages.DeclineRelocationPage(driver);      // create ProductsPage
            declineRelocationPage.Navigate();                            // navigate to url

        }

        [Fact]
        public void Test()
        {
            ChooseParameters();
            Decline();
            requestsCount = declineRelocationPage.RequestsCount();
            Dispose();
        }

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
        }
    }
}
