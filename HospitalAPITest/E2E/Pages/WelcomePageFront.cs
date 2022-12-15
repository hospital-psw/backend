namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class WelcomePageFront
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/";
        private IWebElement logInButton => driver.FindElement(By.XPath("/html/body/app-root/app-welcome-page/div/div[1]/div/a[1]/button"));

        public WelcomePageFront(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);
        public bool logInButtonDisplayed()
        {
            return logInButton.Displayed;
        }
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return logInButton.Displayed;
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

        public void loginButonClick()
        {
            logInButton.Click();
        }
    }
}
