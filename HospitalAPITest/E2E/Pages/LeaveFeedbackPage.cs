namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class LeaveFeedbackPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/app/feedback";

        private IWebElement feedbackText => driver.FindElement(By.Id("feedbackText"));
        private IWebElement feedbackPublic => driver.FindElement(By.Id("publicFeedback"));
        private IWebElement feedbackAnonymous => driver.FindElement(By.Id("anonymousFeedback"));
        private IWebElement feedbackNotCreated => driver.FindElement(By.Id("FeedbackNotCreated"));
        private IWebElement submit => driver.FindElement(By.Id("submitButton"));

        public LeaveFeedbackPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return feedbackText != null && feedbackPublic != null && feedbackAnonymous != null;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
        public bool feedbackTextDisplayed()
        {
            return feedbackText.Displayed;
        }
        public bool feedbackPublicDisplayed()
        {
            return feedbackPublic.Displayed;
        }
        public bool feedbackAnonymousDisplayed()
        {
            return feedbackAnonymous.Displayed;
        } 
        public bool feedbackNotCreatedDisplayed()
        {
            return feedbackNotCreated.Displayed;
        }
        public void insertFeedback(string feedback)
        {
            feedbackText.SendKeys(feedback);
        }
        public void publicClick()
        {
            feedbackPublic.Click();
        }
        public void anonymousClick()
        {
            feedbackAnonymous.Click();
        }
        public void SubmitForm()
        {
            submit.Click();
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(HomePage.URI));
        }
       
    }
}
