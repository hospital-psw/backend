namespace HospitalAPITest.E2E.Pages
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class HomePage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/app/home";
        private IWebElement feedbackButton => driver.FindElement(By.Id("feedback"));

        public HomePage(IWebDriver driver)
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
                    return feedbackButton != null;
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
        public bool feedbackButtonDisplayed()
        {
            return feedbackButton.Displayed;
        }
        public void FeedbackClick()
        {
            feedbackButton.Click();
        }
        public void WaitToRedirectToFeedbackPage()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(LeaveFeedbackPage.URI));
        }
    }
}
