namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class LeaveFeedbackFront
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/feedback";
        private IWebElement commentInput => driver.FindElement(By.XPath("//*[@id=\"mat-input-0\"]"));
        private IWebElement publicInput => driver.FindElement(By.XPath("//*[@id=\"mat-radio-2-input\"]"));
        private IWebElement anonymousInput => driver.FindElement(By.XPath("//*[@id=\"mat-radio-5-input\"]"));
        private IWebElement submitInput => driver.FindElement(By.XPath("/html/body/app-root/app-feedback-page/div/div[1]/app-feedback-form/div/form/div/div/button"));

        public LeaveFeedbackFront(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool commentInputNotNull()
        {
            return commentInput != null;
        }
         
        public bool submitInputDisplayed()
        {
            return submitInput.Displayed;
        }
        public bool publicInputDisplayed()
        {
            return publicInput.Displayed;
        }
        public bool anonymousInputDisplayed()
        {
            return anonymousInput.Displayed;
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return commentInput != null && submitInput.Displayed==true && publicInput.Displayed==true && anonymousInput.Displayed==true;
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
        public void insertMessage(string m)
        {
            commentInput.SendKeys(m);
        }
        public void publicBUttonClick()
        {
            publicInput.Click();
        }
        public void anonymousBUttonClick()
        {
            anonymousInput.Click();
        }
        public void SubmitForm()
        {
            submitInput.Click();
        }
    }
}
