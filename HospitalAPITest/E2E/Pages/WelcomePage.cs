namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class WelcomePage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/";
        private IWebElement loginButton => driver.FindElement(By.Id("loginButton"));
        public WelcomePage(IWebDriver driver)
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
                    return loginButton != null;
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
        public bool loginButtonDisplayed()
        {
            return loginButton.Displayed;
        }
        public void loginButtonClick()
        {
            loginButton.Click();
        }
        public void WaitToRedirectToLoginPage()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(LoginPagePublicApp.URI));
        }
    }
}
