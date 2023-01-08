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

    public class BlockAndUnblockPatientsTest
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.BlockAndUnblockPatientsPage blockAndUnblockPatientsPage;
        private int rowsMal = 0;
        private int rowsBlock = 0;
        private int requestsCount = 0;
        private int newRequestsCount = 0;

        public BlockAndUnblockPatientsTest()
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
            Assert.True(menuPage.blockedPatientsTabDisplayed());
            menuPage.blockedPatientsTabClick();
            blockAndUnblockPatientsPage = new Pages.BlockAndUnblockPatientsPage(driver);
            Assert.True(blockAndUnblockPatientsPage.EnsureTabMalIsDisplayed());
        }

        [Fact]
        public void BlockPatientTest()
        {
            BlockPatient();
            Assert.Equal(rowsMal - 1, blockAndUnblockPatientsPage.MaliciousPatientsCount());
            Dispose();
        }

        [Fact]
        public void UnblockPatientTest()
        {
            UnblockPatient();
            Assert.Equal(rowsBlock - 1, blockAndUnblockPatientsPage.BlockedPatientsCount());
            Dispose();
        }

        [Fact]
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        private void BlockPatient()
        {
            blockAndUnblockPatientsPage.EnsureMalTableIsDisplayed();
            rowsMal = blockAndUnblockPatientsPage.MaliciousPatientsCount();
            blockAndUnblockPatientsPage.ClickBlockButton();
        }
        private void UnblockPatient()
        {
            blockAndUnblockPatientsPage.EnsureTabBlockedIsDisplayed();
            blockAndUnblockPatientsPage.SelectBlockedTab();
            blockAndUnblockPatientsPage.EnsureBlockedTableIsDisplayed();
            rowsBlock = blockAndUnblockPatientsPage.BlockedPatientsCount();
            blockAndUnblockPatientsPage.ClickUnblockButton();
        }
    }
}
