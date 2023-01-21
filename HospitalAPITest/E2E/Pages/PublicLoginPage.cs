namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PublicLoginPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/login";

        private IWebElement emailInput => driver.FindElement(By.Id("email"));
        private IWebElement passwordInput => driver.FindElement(By.Id("pass"));
        private IWebElement submit => driver.FindElement(By.Id("submit"));

        public PublicLoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

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
            submit.Click();
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(PublicHomePage.URI));
        }

    }
}
