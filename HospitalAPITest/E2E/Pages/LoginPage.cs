namespace HospitalAPITest.E2E.Pages
{
    using MySqlX.XDevAPI.Relational;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class LoginPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/";

        private IWebElement emailInput => driver.FindElement(By.XPath("//*[@id=\"mat-input-0\"]"));
        private IWebElement passwordInput => driver.FindElement(By.XPath("//*[@id=\"mat-input-1\"]"));
        private IWebElement logInButton => driver.FindElement(By.XPath("/html/body/app-root/app-login-page/div/div[1]/app-login-form/div/form/button"));
        public string Title => driver.Title;

        public LoginPage(IWebDriver driver)
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
                    return emailInput != null && passwordInput != null;
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

        public bool emailInputDisplayed()
        {
            return emailInput.Displayed;
        }
        public bool passwordInputDisplayed()
        {
            return passwordInput.Displayed;
        }
        public bool loginButtonDisplayed()
        {
            return logInButton.Displayed;
        }
        public void insertEmail(string email)
        {
            emailInput.SendKeys(email);
        }
        public void insertPassword(string password)
        {
            passwordInput.SendKeys(password);
        }
        public void SubmitForm()
        {
            logInButton.Click();
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(MenuPage.URI));
        }

        public void WaitForFormSubmitForDoctor()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(AppointmentsCalendarPage.URI));
        }
    }
}
