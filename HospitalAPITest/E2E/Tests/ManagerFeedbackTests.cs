namespace HospitalAPITest.E2E.Tests
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

    public class ManagerFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.ManagerFeedbackPage managerFeedbackPage;
        public const string URI_APPOINTMENTS = "http://localhost:4200/feedback";

        public ManagerFeedbackTests()
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

            managerFeedbackPage = new Pages.ManagerFeedbackPage(driver);      // create ProductsPage
            managerFeedbackPage.Navigate();                            // navigate to url
            managerFeedbackPage.EnsurePageIsDisplayed();
            managerFeedbackPage.EnsureDataIsFetched();
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        private void Approves_Feedback()
        {
            managerFeedbackPage.GoToPendingFeedbacks();
            managerFeedbackPage.AcceptFeedback();
            bool success = managerFeedbackPage.CheckIfApproved();
            managerFeedbackPage.UndoChanges();
            Assert.True(success);
            Dispose();
        }
    }
}
