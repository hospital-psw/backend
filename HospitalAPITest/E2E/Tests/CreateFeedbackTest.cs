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
    using Xunit;

    public class CreateFeedbackTest
    {
        private readonly IWebDriver driver;
        private Pages.LoginPagePublicApp loginPage;
        private Pages.WelcomePage welcomePage;
        private Pages.HomePage homePage;
        private Pages.LeaveFeedbackPage leaveFeedbackPage;

        public CreateFeedbackTest()
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

            welcomePage = new Pages.WelcomePage(driver);
            welcomePage.Navigate();
            welcomePage.EnsurePageIsDisplayed();
            welcomePage.loginButtonDisplayed();
            welcomePage.loginButtonClick();
            welcomePage.WaitToRedirectToLoginPage();


            loginPage = new Pages.LoginPagePublicApp(driver);
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.loginButtonDisplayed());
            Assert.True(loginPage.emailInputDisplayed());
            Assert.True(loginPage.passwordInputDisplayed());
            loginPage.insertEmail("mitraja@gmail.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            homePage = new Pages.HomePage(driver);
            homePage.EnsurePageIsDisplayed();
            Assert.True(homePage.feedbackButtonDisplayed());
            homePage.FeedbackClick();
            homePage.WaitToRedirectToFeedbackPage();

            leaveFeedbackPage = new Pages.LeaveFeedbackPage(driver);
            leaveFeedbackPage.EnsurePageIsDisplayed();
            Assert.True(leaveFeedbackPage.feedbackTextDisplayed());
            Assert.True(leaveFeedbackPage.feedbackPublicDisplayed());
            Assert.True(leaveFeedbackPage.feedbackAnonymousDisplayed());


        }
        [Fact]
        public void LeaveFeedback()
        {
            leaveFeedbackPage.insertFeedback("very good");
            leaveFeedbackPage.publicClick();
            leaveFeedbackPage.anonymousClick();
            leaveFeedbackPage.SubmitForm();
            leaveFeedbackPage.WaitForFormSubmit();
            Assert.True(homePage.feedbackButtonDisplayed());

            Dispose();
        }
        [Fact]
        public void FailToLeaveFeedback()
        {
            leaveFeedbackPage.insertFeedback("");
            leaveFeedbackPage.publicClick();
            leaveFeedbackPage.anonymousClick();
            leaveFeedbackPage.SubmitForm();
            Assert.True(leaveFeedbackPage.feedbackNotCreatedDisplayed());

            Dispose();
        }

        [Fact]
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

    }
}
