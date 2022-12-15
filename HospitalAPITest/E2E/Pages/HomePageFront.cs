namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium.Support.UI;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using HospitalLibrary.Core.Model;

    internal class HomePageFront
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/home";
        private IWebElement feedbackButton => driver.FindElement(By.XPath("/html/body/app-root/app-navbar/header/nav/ul/li[3]/a"));
        public HomePageFront(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool feedbackButtonDisplayed()
        {
            return feedbackButton.Displayed;
        }
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return feedbackButton.Displayed;
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

        public void feedbackButtonClick()
        {
            feedbackButton.Click();
            Thread.Sleep(6000);
        }
    }
}

