namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PublicHomePage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/home";

        private IWebElement calendar => driver.FindElement(By.TagName("app-calendar"));
        private IWebElement cancelButton => driver.FindElement(By.Id("cancel"));
        private IWebElement appointment;
        private IWebElement yesButton;

        public PublicHomePage(IWebDriver driver)
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
                    return calendar is not null;
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

        public void EnsureModalDialogIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    yesButton = driver.FindElement(By.Id("yes"));
                    return yesButton is not null;
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

        public void selectAppointment()
        {
            appointment = driver.FindElement(By.CssSelector("call-event"));
            appointment.Click();
        }

        public void cancelAppointment()
        {
            cancelButton.Click();
            EnsureModalDialogIsDisplayed();
            yesButton.Click();
        }
    }
}
