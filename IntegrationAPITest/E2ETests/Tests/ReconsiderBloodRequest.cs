namespace IntegrationAPITest.E2ETests.Tests
{
    using IntegrationAPITest.E2ETests.Pages;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReconsiderBloodRequest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.ReconsiderBloodRequestPage reconsiderBloodRequestPage;

        public ReconsiderBloodRequest()
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
            menuPage.reconsiderRequestTabClick();

            reconsiderBloodRequestPage = new Pages.ReconsiderBloodRequestPage(driver);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void MakeAccepted_ClickButton_AcceptsRequest()
        {
            driver.Navigate().GoToUrl("http://localhost:4200/app/blood-request-view");
            Thread.Sleep(2000);
            var table = driver.FindElement(By.CssSelector("table"));

            var acceptButton = table.FindElement(By.CssSelector("button.buttonAccept"));
            Thread.Sleep(2000);
            acceptButton.Click();
            Thread.Sleep(2000);
            var statusDiv = table.FindElement(By.Id("accepted"));
            Thread.Sleep(2000);
            Assert.Equal("ACCEPTED", statusDiv.Text);

            Dispose();

        }

        [Fact]
        public void MakeDeclined_ClickButton_DeclinesRequest()
        {
            driver.Navigate().GoToUrl("http://localhost:4200/app/blood-request-view");
            Thread.Sleep(2000);
            var table = driver.FindElement(By.CssSelector("table"));

            var acceptButton = table.FindElement(By.CssSelector("button.buttonDecline"));
            Thread.Sleep(2000);
            acceptButton.Click();
            Thread.Sleep(2000);
            var statusDiv = table.FindElement(By.Id("declined"));
            Thread.Sleep(2000);
            Assert.Equal("DECLINED", statusDiv.Text);

            Dispose();

        }

        [Fact]
        public void MakeReconsider_ClickButton_ReconsidersRequest()
        {
            driver.Navigate().GoToUrl("http://localhost:4200/app/blood-request-view");
            Thread.Sleep(2000);
            var table = driver.FindElement(By.CssSelector("table"));

            var reconsiderButton = table.FindElement(By.CssSelector("button.buttonReconsider"));
            Thread.Sleep(2000);
            reconsiderButton.Click();
            Thread.Sleep(2000);
            var statusDiv = table.FindElement(By.Id("reconsider"));
            Thread.Sleep(2000);
            Assert.Equal("RECONSIDERED", statusDiv.Text);

            Dispose();

        }
    }
}
