namespace HospitalAPITest.E2E.Tests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateFeedbackTest
    {
        private readonly IWebDriver driver;
        private Pages.WelcomePageFront welcomePageFront;
        private Pages.LoginPageFront loginPageFront;
        private Pages.HomePageFront homePageFront;
        private Pages.LeaveFeedbackFront leaveFeedbackFront;

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
            welcomePageFront = new Pages.WelcomePageFront(driver);
            welcomePageFront.Navigate();
            Assert.True(welcomePageFront.logInButtonDisplayed());
            welcomePageFront.loginButonClick();
            loginPageFront= new Pages.LoginPageFront(driver);
            Assert.True(loginPageFront.loginButtonDisplayed());
            Assert.True(loginPageFront.emailInputDisplayed());
            Assert.True(loginPageFront.passwordInputDisplayed());
            loginPageFront.insertEmail("mitraja@gmail.com");
            loginPageFront.insertPassword("123.Auth");
            loginPageFront.SubmitForm();
            //loginPageFront.WaitForFormSubmit();
            homePageFront= new Pages.HomePageFront(driver);
            homePageFront.feedbackButtonClick();
            Assert.True(homePageFront.feedbackButtonDisplayed());
            leaveFeedbackFront= new Pages.LeaveFeedbackFront(driver);
           // Assert.True(leaveFeedbackFront.anonymousInputDisplayed());
           // Assert.True(leaveFeedbackFront.publicInputDisplayed());
            Assert.True(leaveFeedbackFront.submitInputDisplayed());
            Assert.True(leaveFeedbackFront.commentInputNotNull());

        }



        [Fact]
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void create_feedback()
        {
            leaveFeedbackFront.insertMessage("goodgood");
            leaveFeedbackFront.publicBUttonClick();
            leaveFeedbackFront.anonymousBUttonClick();
            leaveFeedbackFront.SubmitForm();
            Assert.True(homePageFront.feedbackButtonDisplayed());
            Dispose();
        }
        
    }
}
